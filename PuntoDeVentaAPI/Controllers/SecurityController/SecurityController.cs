using AutoMapper;
using LinkQuality.Api.Services;

using LinkQuality.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Data;
using Data.Interfaces.SecurityInterfaces;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Entities.Security;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Dto.SecurityDTO;
using PuntoDeVentaData.Entities.Enumerators;

namespace PuntoDeVentaAPI.Controllers.SecurityController
{
    [Route("api/Security")]
    public class SecurityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuditoriaAccesosRepository _auditoriaAccesos;
        private readonly ApplicationUserManager _userManager;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDataProtector dataProtector;
        private string JWTKey { get; set; }
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("SecurityController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        public readonly string _ip;
        private readonly string _nombreController;


        public SecurityController(IDataProtectionProvider dataProtectionProvider,ApplicationDbContext applicationDbContext, IAuditoriaAccesosRepository auditoriaAccesos, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, SignInManager<ApplicationUser> signInManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            this._context = applicationDbContext;
            _auditoriaAccesos = auditoriaAccesos;
            _userManager = userManager;
            _service = service;
            JWTKey = configuration["JWTKey"];
            dataProtector = dataProtectionProvider.CreateProtector(JWTKey);
            _mapper = mapper;
            _configuration = configuration;
            this.signInManager = signInManager;
            _nombreController = "SecurityController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;

        }



        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] CredencialesDTO credenciales)
        {

            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByNameAsync(credenciales.UserName);

                if (usuario == null)
                {
                    return StatusCode(404, "El usuario ingresado no se encuentra registrado.");
                }


                bool isSuscced = await _userManager.CheckPasswordAsync(usuario, credenciales.Password);
                if (isSuscced)
                {
                    isSuscced = await _userManager.IsLockedOutAsync(usuario);
                    if (isSuscced)
                    {
                        infoDTO.AccionFallida("Cuenta bloqueada temporalmente", 401);
                        infoDTO.Detail = "Tu cuenta está bloqueada debido a demasiados intentos fallidos de inicio de sesión. Inténtalo nuevamente dentro de 1 minuto.";
                        return BadRequest(infoDTO);

                    }
                    AuditoryAccess aud = new AuditoryAccess();
                    aud.Type =  EnumAccessType.LogIn;
                    aud.User = credenciales.UserName;
                    aud.DateAdmission = DateTime.Now;
                    aud.DateRegister = DateTime.Now;
                    aud.UserRegister = credenciales.UserName;
                    aud.IpRegister = _ip ?? "-";
                    aud.Active = true;
                    await _auditoriaAccesos.RegistrarAuditoriaAccesos(aud);

                    var token = await CreateToken(usuario.UserName);
                    return Ok(token);
                }
                else
                {
                    // En caso de credenciales incorrectas
                    infoDTO.AccionFallida("Credenciales incorrectas", 400);

                    // Obtener el número de intentos restantes
                    var intentosRestantes = await _userManager.GetAccessFailedCountAsync(usuario);
                    var maxIntentos = _userManager.Options.Lockout.MaxFailedAccessAttempts;
                    infoDTO.Detail = $"El usuario o la contraseña son incorrectos, Intentos restantes:  {maxIntentos - intentosRestantes}";

                    return BadRequest(infoDTO);

                }
            }
            else
            {
                ModelState.AddModelError(null, "Los campos son obligatorios");
                return BadRequest("Credenciales incorrectas");
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogOut(CredencialesLogOutDto credenciales)
        {
            AuditoryAccess aud = new AuditoryAccess();
            aud.Type = EnumAccessType.LogOut;
            aud.User = credenciales.UserName;
            aud.DateAdmission = DateTime.Now;
            aud.DateRegister = DateTime.Now;
            aud.UserRegister = credenciales.UserName;
            aud.IpRegister = credenciales.Ip ?? "-";
            aud.Active = true;

            await _auditoriaAccesos.RegistrarAuditoriaAccesos(aud);

            return Ok("Success");
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Registrar([FromBody] RegistrarUsuarioDto registerInfo) 
        {
            if(_context.Users.Where(x => x.UserName.Equals(registerInfo.UserName)).Any())
            {
                infoDTO.Message = $"El usuario {registerInfo.UserName} ya se encuentra registrado";
                return BadRequest(infoDTO);
            }
            var usuario = new ApplicationUser { UserName = registerInfo.UserName, Email = registerInfo.UserName, Identificacion = registerInfo.Identificacion, PhoneNumber = registerInfo.PhoneNumber, FirstName = registerInfo.FirstName, LastName = registerInfo.LastName };
            var resultado = await _userManager.CreateAsync(usuario, registerInfo.Password);
            if (resultado.Succeeded)
            {

                var idRol = await _context.Roles
                    .Where(rol => rol.Name.Contains(registerInfo.RolName))
                    .Select(rol => rol.Id)
                    .FirstOrDefaultAsync();

                await _context.AddAsync(new IdentityUserRole<string>
                {
                    UserId = usuario.Id,
                    RoleId = idRol
                });

                await _context.SaveChangesAsync();

                var token = await CreateToken(usuario.UserName);
                return Ok(token);
            }
            else
            {
                infoDTO.Message = "Error al registar usuario";
                infoDTO.Detail = $"Message: {string.Join(",", resultado.Errors)}";
                foreach (var error in resultado.Errors)
                {
                    infoDTO.Detail += $"{resultado.Errors?.FirstOrDefault()?.Code}|{resultado.Errors?.FirstOrDefault()?.Description}";
                    _log.Error(infoDTO.Message, resultado.Errors);
                }
                return BadRequest(infoDTO);
            }

        }


        [HttpPost("RegisterByAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> RegisterByAdmin([FromBody] RegistrarUsuarioDto registerInfo)
        {
            if (_context.Users.Where(x => x.UserName.Equals(registerInfo.UserName)).Any())
            {
                infoDTO.Message = $"El usuario {registerInfo.UserName} ya se encuentra registrado";
                return BadRequest(infoDTO);
            }
            var usuario = new ApplicationUser { UserName = registerInfo.UserName, Email = registerInfo.UserName, Identificacion = registerInfo.Identificacion, PhoneNumber = registerInfo.PhoneNumber, FirstName = registerInfo.FirstName, LastName = registerInfo.LastName };
            var resultado = await _userManager.CreateAsync(usuario, registerInfo.Password);
            if (resultado.Succeeded)
            {

                var idRol = await _context.Roles
                    .Where(rol => rol.Name.Contains(registerInfo.RolName))
                    .Select(rol => rol.Id)
                    .FirstOrDefaultAsync();

                await _context.AddAsync(new IdentityUserRole<string>
                {
                    UserId = usuario.Id,
                    RoleId = idRol
                });

                await _context.SaveChangesAsync();

                var token = await CreateToken(usuario.UserName);
                return Ok(token);
            }
            else
            {
                infoDTO.Message = "Error al registar usuario";
                infoDTO.Detail = $"Message: {string.Join(",", resultado.Errors)}";
                foreach (var error in resultado.Errors)
                {
                    infoDTO.Detail += $"{resultado.Errors?.FirstOrDefault()?.Code}|{resultado.Errors?.FirstOrDefault()?.Description}";
                    _log.Error(infoDTO.Message, resultado.Errors);
                }
                return BadRequest(infoDTO);
            }

        }


        [HttpPut]
        [Route("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ChangePassword([FromBody] CredencialesDTO credencialesDTO)
        {
            var ip = _ip ?? "-";
            infoDTO = await _userManager.ModificarContrasenia(credencialesDTO, ip);
            return StatusCode(infoDTO.Status, infoDTO);
        }

        [HttpPut]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword([FromBody] RecuperarContraseniaDTO recuperarContraseniaDTO)
        {
            var ip = _ip ?? "-";
            infoDTO = await _userManager.RecuperarContrasenia(recuperarContraseniaDTO, ip);
            return StatusCode(infoDTO.Status, infoDTO);
        }


        #region Metodos privados
        private Task<string> ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JWTKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "email").Value;

                // return user id from JWT token if validation successful
                return Task.FromResult(userId);
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        private async Task<RespuestaAutenticacion> CreateToken(string username)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", username),
            };

            var user = await _userManager.FindByNameAsync(username);
            var claimsDB = await _userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var idRol = await _context.UserRoles
                        .Where(userRoles => userRoles.UserId.Contains(user.Id))
                        .Select(userRoles => userRoles.RoleId).FirstOrDefaultAsync();
            var RoleName = await _context.Roles
                .Where(rol => rol.Id.Contains(idRol))
                .Select(rol => rol.Name).FirstOrDefaultAsync();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);

            var securtityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securtityToken),
                Expiracion = expiration,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = username,
                Role = RoleName ?? "",
            };
        }

        private MessageInfoDTO ChooseDataBase(string DataBaseName)
        {
            //Seteo la base de datos a la cadena de conexión
            _context.Database.SetConnectionString(_configuration.GetConnectionString("DefaultConnection")?.Replace("NominaDB", DataBaseName));

            return infoDTO.AccionCompletada("Seleccion de BD correcta");
        }

        #endregion
    }
}
