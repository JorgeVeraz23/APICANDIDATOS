using AutoMapper;
using Data.Interfaces.ApiCandidatosInterfaces;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using Data.Dto.PropuestaDTO;
using System.Net;
using Data.Dto.RedSocialDTO;

namespace PuntoDeVentaAPI.Controllers.RedSocialController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RedSocialController : ControllerBase
    {

        private readonly RedSocialInterface _redSocialInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("RedSocialController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;


        public RedSocialController(RedSocialInterface redSocialInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _redSocialInterface = redSocialInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "RedSocialController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }

        [HttpGet]
        [Route("GetAllRedSocial")]
        public async Task<ActionResult> GetAllRedSocial()
        {
            try
            {
                var result = await _redSocialInterface.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar la red social"));
            }
        }

        [HttpPost]
        [Route("CrearRedSocial")]
        public async Task<ActionResult> CrearRedSocial(RedSocialDTO redSocial)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var resultSave = await _redSocialInterface.Create(redSocial);
                if (resultSave.Success)
                {
                    return Ok(new MessageInfoDTO().AccionCompletada(resultSave.Message ?? string.Empty));
                }
                else
                {
                    return BadRequest(new MessageInfoDTO().AccionFallida(resultSave.Message ?? string.Empty, (int)HttpStatusCode.BadRequest));

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear la red social"));
            }
        }

        [HttpGet]
        [Route("GetRedSocial")]
        public async Task<ActionResult> GetRedSocial(long IdRedSocial)
        {
            try
            {
                var result = await _redSocialInterface.Get(IdRedSocial);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar la red social seleccionada"));
            }
        }


        [HttpDelete]
        [Route("EliminarRedSocial")]
        public async Task<ActionResult> EliminarRedSocial(long IdRedSocial)
        {
            try
            {
                var resultDelete = await _redSocialInterface.Desactive(IdRedSocial);
                if (resultDelete.Success)
                {
                    return Ok(resultDelete.Success);
                }
                else
                {
                    return BadRequest(resultDelete.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar la red social"));
            }
        }

        [HttpPut]
        [Route("ActualizarRedSocial")]
        public async Task<ActionResult> ActualizarRedSocial(RedSocialDTO redSocial)
        {
            try
            {
                var resultSave = await _redSocialInterface.Edit(redSocial);
                if (resultSave.Success)
                {
                    return Ok(resultSave.Success);
                }
                else
                {
                    return BadRequest(resultSave.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al actualizar la red social"));
            }
        }

    }
}
