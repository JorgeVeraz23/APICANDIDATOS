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
using Data.Dto.EventoDTO;
using System.Net;
using Data.Dto.PartidoPoliticoDTO;

namespace PuntoDeVentaAPI.Controllers.PartidoController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartidoController : ControllerBase
    {

        private readonly PartidoInterface _partidoInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("PartidoController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;

        public PartidoController(PartidoInterface partidoInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _partidoInterface = partidoInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "PartidoController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }

        [HttpGet]
        [Route("GetAllPartido")]
        public async Task<ActionResult> GetAllPartido()
        {
            try
            {
                var result = await _partidoInterface.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los partidos"));
            }
        }

        [HttpPost]
        [Route("CrearPartidos")]
        public async Task<ActionResult> CrearPartidos(PartidoDTO partido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var resultSave = await _partidoInterface.Create(partido);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear los partidos"));
            }
        }

        [HttpGet]
        [Route("GetPartidos")]
        public async Task<ActionResult> GetPartidos(long IdPartido)
        {
            try
            {
                var result = await _partidoInterface.Get(IdPartido);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar el partido seleccionado"));
            }
        }


        [HttpDelete]
        [Route("EliminarPartido")]
        public async Task<ActionResult> EliminarPartido(long IdPartidos)
        {
            try
            {
                var resultDelete = await _partidoInterface.Desactive(IdPartidos);
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
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar el partido"));
            }
        }

        [HttpPut]
        [Route("ActualizarPartido")]
        public async Task<ActionResult> ActualizarPartido(PartidoDTO partido)
        {
            try
            {
                var resultSave = await _partidoInterface.Edit(partido);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al actualizar los partidos"));
            }
        }


        [HttpGet]
        [Route("KeyValuePartido")]
        public async Task<ActionResult> KeyValuePartido()
        {
            try
            {
                var resultKeyValueCargo = await _partidoInterface.KeyValuePartido();
                return Ok(resultKeyValueCargo);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
