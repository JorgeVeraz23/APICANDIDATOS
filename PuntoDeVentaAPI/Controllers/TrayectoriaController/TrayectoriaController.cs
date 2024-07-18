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
using Data.Dto.RedSocialDTO;
using System.Net;
using Data.Dto.ExperienciaDTO;

namespace PuntoDeVentaAPI.Controllers.TrayectoriaController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrayectoriaController : ControllerBase
    {

        private readonly TrayectoriaInterface _trayectoriaInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("TrayectoriaController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;


        public TrayectoriaController(TrayectoriaInterface trayectoriaInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _trayectoriaInterface = trayectoriaInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "TrayectoriaController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }

        [HttpGet]
        [Route("GetAllTrayectoria")]
        public async Task<ActionResult> GetAllTrayectoria()
        {
            try
            {
                var result = await _trayectoriaInterface.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar la trayectoria"));
            }
        }

        [HttpPost]
        [Route("CrearTrayectoria")]
        public async Task<ActionResult> CrearTrayectoria(TrayectoriaDTO trayectoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var resultSave = await _trayectoriaInterface.Create(trayectoria);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear la trayectoria"));
            }
        }

        [HttpGet]
        [Route("GetTrayectoria")]
        public async Task<ActionResult> GetTrayectoria(long IdTrayectoria)
        {
            try
            {
                var result = await _trayectoriaInterface.Get(IdTrayectoria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar la trayectoria"));
            }
        }


        [HttpDelete]
        [Route("EliminarTrayectoria")]
        public async Task<ActionResult> EliminarTrayectoria(long IdTrayectoria)
        {
            try
            {
                var resultDelete = await _trayectoriaInterface.Desactive(IdTrayectoria);
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
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar la trayectoria"));
            }
        }

        [HttpPut]
        [Route("ActualizarTrayectoria")]
        public async Task<ActionResult> ActualizarTrayectoria(TrayectoriaDTO trayectoria)
        {
            try
            {
                var resultSave = await _trayectoriaInterface.Edit(trayectoria);
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
