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
using Data.Dto.PartidoPoliticoDTO;
using System.Net;
using Data.Dto.PropuestaDTO;

namespace PuntoDeVentaAPI.Controllers.PropuestaController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropuestaController : ControllerBase
    {
        private readonly PropuestaInterface _propuestaInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("PropuestaController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;

        public PropuestaController(PropuestaInterface propuestaInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _propuestaInterface = propuestaInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "PropuestaController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }

        [HttpGet]
        [Route("GetAllPropuesta")]
        public async Task<ActionResult> GetAllPropuesta()
        {
            try
            {
                var result = await _propuestaInterface.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar las propuestas"));
            }
        }

        [HttpPost]
        [Route("CrearPropuesta")]
        public async Task<ActionResult> CrearPropuesta(PropuestaDTO propuesta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var resultSave = await _propuestaInterface.Create(propuesta);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear las propuestas"));
            }
        }

        [HttpGet]
        [Route("GetPropuesta")]
        public async Task<ActionResult> GetPropuesta(long IdPropuesta)
        {
            try
            {
                var result = await _propuestaInterface.Get(IdPropuesta);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar la propuesta seleccionado"));
            }
        }


        [HttpDelete]
        [Route("EliminarPropuesta")]
        public async Task<ActionResult> EliminarPropuesta(long IdPropuesta)
        {
            try
            {
                var resultDelete = await _propuestaInterface.Desactive(IdPropuesta);
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
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar la propuesta"));
            }
        }

        [HttpPut]
        [Route("ActualizarPropuesta")]
        public async Task<ActionResult> ActualizarPropuesta(PropuestaDTO propuesta)
        {
            try
            {
                var resultSave = await _propuestaInterface.Edit(propuesta);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al actualizar las propuestas"));
            }
        }


    }
}
