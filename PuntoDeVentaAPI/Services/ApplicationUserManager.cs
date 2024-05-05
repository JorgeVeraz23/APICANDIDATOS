using Data;
using Data.Interfaces.TemplateInterfaces;
using LinkQuality.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PuntoDeVentaData.Dto.SecurityDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System.Security.Claims;
using static LinkQuality.Api.Services.EmailService;

namespace PuntoDeVentaAPI.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly RoleManager<IdentityRole> _roleManager;
        private MessageInfoDTO infoDTO = new();
        private readonly MailRepository _emailManager;
        private readonly IEmailTemplate _plantillaCorreo;
        private readonly ApplicationDbContext _context;
        public ApplicationUserManager(IConfiguration configuration, IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor, ApplicationDbContext context, IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger,
            RoleManager<IdentityRole> roleManager, IEmailTemplate plantillaCorreo)
                : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userStore = store;
            _context = context;
            _roleManager = roleManager;
            this._emailManager = new MailRepository(configuration);
            _plantillaCorreo = plantillaCorreo;
        }
        // Método personalizado para buscar usuario por correo electrónico

        public async Task<ApplicationUser?> FindByUserAsyncCustom(string userName)
        {
            return await Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<ApplicationUser?> FindByIdUserAsync(string id)
        {
            return await Users.SingleOrDefaultAsync(u => u.Id == id);

        }

        public async Task<UserDataDTO> FindByUserNameAsyncCustom(string userName)
        {
            var u = await Users.SingleOrDefaultAsync(u => u.UserName == userName);
            var roles = await GetRolesAsync(u);
            var rolesList = roles.ToList();

            var userData = new UserDataDTO
            {
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Bloqueo = u.Bloqueo,
                PhoneNumber = u.PhoneNumber,
                DateRegister = u.FechaRegistro,
                DateModification = u.FechaModificacion ?? u.FechaRegistro,
                Active = u.Activo.Value,
                rolName = string.Join(", ", rolesList),
            };

            return userData;
        }
        public async Task<ApplicationUser> FindByEmailAsyncCustom(string email)
        {
            return await Users.SingleOrDefaultAsync(u => u.Email == email);
        }
        public async Task<List<UserDataDTO>> GetAllUsers()
        {
            try
            {
                List<ApplicationUser> Usuarios = await Users.Where(u => u.Activo.Value).ToListAsync();

                List<UserDataDTO> userDataList = new List<UserDataDTO>();

                foreach (var u in Usuarios)
                {
                    var roles = await GetRolesAsync(u);
                    var rolesList = roles.ToList();

                    var userData = new UserDataDTO
                    {
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Bloqueo = u.Bloqueo,
                        PhoneNumber = u.PhoneNumber,
                        DateRegister = u.FechaRegistro,
                        DateModification = u.FechaModificacion ?? u.FechaRegistro,
                        Active = u.Activo.Value,
                        rolName = string.Join(", ", rolesList),
                    };

                    userDataList.Add(userData);
                }

                return userDataList;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            IdentityRole role = new IdentityRole(roleName);
            IdentityResult result = await _roleManager.CreateAsync(role);
            return result;
        }
        public async Task<IdentityResult> UpdateRole(string roleId, string newRoleName)
        {
            try
            {
                IdentityRole role = await _roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    role.Name = newRoleName;
                    IdentityResult result = await _roleManager.UpdateAsync(role);
                    return result;
                }
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<DropDownWebDTO>> GetAllRolesDrop()
        {
            try
            {
                List<DropDownWebDTO> roles = await _roleManager.Roles.Select(r => new DropDownWebDTO
                {
                    Text = r.Name,
                    Value = r.Name,
                }).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<IdentityRole>> GetAllRoles()
        {
            try
            {
                List<IdentityRole> roles = await _roleManager.Roles.Select(r => new IdentityRole
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> GetRole(ApplicationUser user)
        {
            try
            {
                // Obtenemos el rol del usuario usando el UserManager
                IList<string> userRoles = await GetRolesAsync(user);

                // Suponemos que el usuario tiene un único rol asignado
                if (userRoles.Count == 1)
                {
                    // Obtenemos el primer y único rol del usuario
                    string userRole = userRoles.First();
                    return userRole;
                }
                else
                {
                    // Si el usuario no tiene roles o tiene más de uno, retornamos un valor por defecto o manejas el caso según tu lógica de negocio.
                    return "DefaultRole"; // Por ejemplo, puedes retornar un valor por defecto.
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IdentityResult> AssignRoleToUserAsync(ApplicationUser user, string roleName)
        {
            try
            {
                bool roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    // El rol no existe, puedes manejar el error como consideres adecuado
                    return IdentityResult.Failed(new IdentityError { Code = "RoleNotFound", Description = "El rol especificado no existe." });
                }

                // Asegurarnos de que el usuario no tenga ya el rol antes de asignarlo
                IList<string> userRoles = await GetRolesAsync(user);
                if (userRoles.Contains(roleName))
                {
                    // El usuario ya tiene el rol, no es necesario asignarlo de nuevo
                    return IdentityResult.Success;
                }

                // Agregar el rol al usuario
                IdentityResult result = await AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    // Asignar el RoleClaim al usuario
                    Claim roleClaim = new Claim("Rol", roleName);
                    result = await AddClaimAsync(user, roleClaim);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IdentityResult> EditUserFields(ApplicationUser user, UserDataDTO Update = null)
        {
            user.FirstName = Update.FirstName;
            user.LastName = Update.LastName;
            user.Email = Update.Email;
            user.UserName = Update.UserName;
            user.Identificacion = Update.Identificacion;
            var result = await UpdateAsync(user);
            return result;
        }
        public async Task<IdentityResult> DeleteUser(ApplicationUser user)
        {
            user.Activo = false;
            user.FechaEliminacion = DateTime.Now;

            var result = await UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> ActivateUser(ApplicationUser user)
        {
            user.Activo = true;
            user.FechaModificacion = DateTime.Now;

            var result = await UpdateAsync(user);
            return result;
        }


        public async Task<MessageInfoDTO> ActivateDesactivateInspectorSelected(string UserName)
        {
            MessageInfoDTO messageInfoDTO = new MessageInfoDTO();


            var inspectorSelected = await FindByUserAsyncCustom(UserName);

            if (inspectorSelected == null)
            {
                messageInfoDTO.AccionFallida("El usuario seleccionado no se encuentra disponible", 400);
                return messageInfoDTO;
            }

            //mismo metodo para activar o desactivar
            if (inspectorSelected.Activo == false)
            {
                await ActivateUser(inspectorSelected);
                messageInfoDTO.AccionCompletada("Inspector activado correctamente");

            }
            else
            {
                await DeleteUser(inspectorSelected);
                messageInfoDTO.AccionCompletada("Inspector desactivado correctamente");
            }



            return messageInfoDTO;

        }


        public async Task<MessageInfoDTO> ModificarContrasenia(CredencialesDTO credencialesDTO, string ip)
        {
            try
            {
                ApplicationUser user = await FindByNameAsync(credencialesDTO.UserName);
                if (user == null)
                {
                    return infoDTO.AccionFallida("No se encontro el usuario", 404);
                }
                user.PasswordHash = PasswordHasher.HashPassword(user, credencialesDTO.Password);
                user.FechaModificacion = DateTime.Now;
                user.UsuarioModificacion = credencialesDTO.UserName;
                user.IpModificacion = ip;
                var result = await UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return infoDTO.AccionFallida("Ocurrio un error al cambiar la contraseña", 400);

                }
                return infoDTO.AccionCompletada("Se cambio la contasenia exitosamente");
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "AplicationUserManager", "Error al cambiar la contraseña");
            }
        }

        public async Task<MessageInfoDTO> RecuperarContrasenia(RecuperarContraseniaDTO recuperarContraseniaDTO, string ip)
        {

            try
            {
                ApplicationUser user = await FindByNameAsync(recuperarContraseniaDTO.UserName);
                if (user == null)
                {
                    return infoDTO.AccionFallida("No se encontro el usuario", 404);
                }
                bool verificarEmail = String.Equals(user.Email, recuperarContraseniaDTO.UserName, StringComparison.OrdinalIgnoreCase);
                if (!verificarEmail)
                {
                    return infoDTO.AccionFallida("El correo no coincide con el registrado", 400);
                }
                var contrasenia = GenerarContrasenia();
                user.PasswordHash = PasswordHasher.HashPassword(user, contrasenia);

                user.FechaModificacion = DateTime.Now;
                user.UsuarioModificacion = recuperarContraseniaDTO.UserName;
                user.IpModificacion = ip;

                var result = await UpdateAsync(user);
                string[] patametros = { recuperarContraseniaDTO.UserName, contrasenia };
                var envioCorreo = await _emailManager.SendEmail(recuperarContraseniaDTO.UserName, "Recuperar Cuenta", EmailMessageOnly.TemplateForgotPassword(recuperarContraseniaDTO.UserName, contrasenia));
                return infoDTO.AccionCompletada("todo bien");
                //return await EnviarEmail(recuperarContraseniaDTO.Email, "recuperarContrasenia", patametros);

            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "ApplicationUserManager", "Error al recuperar la contraseña.");
            }


        }

        //public async Task<MessageInfoDTO> NotificarInspeccion(NotificarInspeccionDto notificarInspeccionDto, string ip)
        //{
        //    try
        //    {
        //        ApplicationUser user = await FindByNameAsync(notificarInspeccionDto.UserName);
        //        if (user == null)
        //        {
        //            return infoDTO.AccionFallida("No se encontro el usuario", 400);
        //        }
        //        bool verificarEmail = String.Equals(user.Email, notificarInspeccionDto.UserName, StringComparison.OrdinalIgnoreCase);
        //        if (!verificarEmail)
        //        {
        //            return infoDTO.AccionFallida("El correo no coincide con el registrado", 400);
        //        }

        //        user.FechaModificacion = DateTime.Now;
        //        user.UsuarioModificacion = notificarInspeccionDto.UserName;
        //        user.IpModificacion = ip;

        //        var envioCorreo = await _emailManager.SendEmail(notificarInspeccionDto.UserName, "NotificarInspección", EmailMessageOnly.TemplateRegisterInspectionNotification(notificarInspeccionDto.UserName, notificarInspeccionDto.TypeInspection, notificarInspeccionDto.NomgreInspector));
        //        return infoDTO.AccionCompletada("Se ha notificado la inspección realizada");
        //    }catch(Exception ex)
        //    {
        //        return infoDTO.ErrorInterno(ex, "ApplicationUserManager", "Error al notificar la inspección");
        //    }
        //}
        //public async Task<MessageInfoDTO> NotificarInspeccion(NotificarInspeccionDto notificarInspeccionDto, string _ip)
        //{
        //    try
        //    {
        //        ApplicationUser user = await FindByNameAsync(notificarInspeccionDto.UserName);
        //        if (user == null)
        //        {
        //            return infoDTO.AccionFallida("No se encontro el usuario", 404);
        //        }


        //        var parametros = await _context.Parameters.FirstOrDefaultAsync();
        //        if (parametros == null)
        //        {
        //            return infoDTO.AccionFallida("No se encontró ningún valor en la tabla de parámetros para EmailList", 400);
        //        }
        //        var Nombre = user.FirstName;
        //        string emailList = parametros.EmailList;
        //        foreach (var correo in emailList)
        //        {
        //            var email = correo;
        //            var envioCorreo = await _emailManager.SendEmail(email, "NotificarInspección", EmailMessageOnly.TemplateRegisterInspectionNotification(email = correo, notificarInspeccionDto.TypeInspection, notificarInspeccionDto.NomgreInspector = Nombre));
        //        }

        //        return infoDTO.AccionCompletada("Se ha notificado la inspección realizada a todos los usuarios");
        //    }
        //    catch (Exception ex)
        //    {
        //        return infoDTO.ErrorInterno(ex, "ApplicationUserManager", "Error al notificar la inspección");
        //    }
        //}



        private string GenerarContrasenia()
        {
            string contrasenia = "";
            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                char letraMinuscula = (char)random.Next('a', 'z' + 1);
                contrasenia += letraMinuscula;
            }

            contrasenia += "#";

            for (int i = 0; i < 2; i++)
            {
                char digito = (char)random.Next('0', '9' + 1);
                contrasenia += digito;
            }
            contrasenia += (char)random.Next('A', 'Z' + 1);
            return contrasenia;
        }


    }
}
