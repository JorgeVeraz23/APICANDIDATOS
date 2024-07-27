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
using Data.Dto.ExperienciaDTO;
using System.Net;
using Data.Dto.TransparienciaDTO;

namespace PuntoDeVentaAPI.Controllers.TransparienciaController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransparienciaController : ControllerBase
    {

        private readonly TransparienciaInterface _transparienciaInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("TransparienciaController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;


        public TransparienciaController(TransparienciaInterface transparienciaInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _transparienciaInterface = transparienciaInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "TransparienciaController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }


        [HttpGet]
        [Route("GetAllTranspariencia")]
        public async Task<ActionResult> GetAllTranspariencia()
        {
            try
            {
                var result = await _transparienciaInterface.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar la transpariencia"));
            }
        }

        [HttpPost]
        [Route("CrearTranspariencia")]
        public async Task<ActionResult> CrearTranspariencia(TransparienciaDTO transpariencia)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var resultSave = await _transparienciaInterface.Create(transpariencia);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear la transpariencia"));
            }
        }

        [HttpGet]
        [Route("GetTranspariencia")]
        public async Task<ActionResult> GetTranspariencia(long IdTranspariencia)
        {
            try
            {
                var result = await _transparienciaInterface.Get(IdTranspariencia);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar la transpariencia"));
            }
        }


        [HttpDelete]
        [Route("EliminarTranspariencia")]
        public async Task<ActionResult> EliminarTranspariencia(long IdTranspariencia)
        {
            try
            {
                var resultDelete = await _transparienciaInterface.Desactive(IdTranspariencia);
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
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar la transpariencia"));
            }
        }

        [HttpPut]
        [Route("ActualizarTranspariencia")]
        public async Task<ActionResult> ActualizarTranspariencia(TransparienciaDTO transpariencia)
        {
            try
            {
                var resultSave = await _transparienciaInterface.Edit(transpariencia);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al actualizar la transpariencia"));
            }
        }


        [HttpGet]
        [Route("KeyValueTransparencia")]
        public async Task<ActionResult> KeyValueTransparencia()
        {
            try
            {
                var resultKeyValueTransparencia = await _transparienciaInterface.KeyValueTransparencia();
                return Ok(resultKeyValueTransparencia);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
