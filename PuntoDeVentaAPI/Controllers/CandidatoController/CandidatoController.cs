using AutoMapper;
using Data;
using Data.Dto.CandidatoDTO;
using Data.Interfaces.ApiCandidatosInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System.Net;

namespace PuntoDeVentaAPI.Controllers.CandidatoController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CandidatoController : ControllerBase
    {
        private readonly CandidatoInterface _candidatoInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("MotivoController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;



        public CandidatoController(CandidatoInterface candidatoInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _candidatoInterface = candidatoInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "CandidatoController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }

        [HttpGet]
        [Route("GetAllCandidato")]
        public async Task<ActionResult> GetAllCandidato()
        {
            try
            {
                var result = await _candidatoInterface.GetAll();
                return Ok(result);
            }catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los candidatos"));
            }
        }


        [HttpGet]
        [Route("KeyValueCandidato")]
        public async Task<ActionResult> KeyValueCandidato()
        {
            try
            {
                var result = await _candidatoInterface.KeyValue();
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los candidatos en el selector"));
            }
        }

        [HttpPost]
        [Route("CrearCandidato")]
        public async Task<ActionResult> CrearCandidato(CandidatoDTO candidato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var resultSave = await _candidatoInterface.Create(candidato);
                if (resultSave.Success)
                {
                    return Ok(new MessageInfoDTO().AccionCompletada(resultSave.Message ?? string.Empty));
                }
                else
                {
                    return BadRequest(new MessageInfoDTO().AccionFallida(resultSave.Message ?? string.Empty, (int)HttpStatusCode.BadRequest));

                }
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear los candidatos"));
            }
        }

        [HttpGet]
        [Route("GetCandidatos")]
        public async Task<ActionResult> GetCandidatos(long IdCandidato)
        {
            try
            {
                var result = await _candidatoInterface.Get(IdCandidato);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar el candidato seleccionado"));
            }
        }


        [HttpGet]
        [Route("GetCandidatoConDetalles")]
        public async Task<ActionResult> GetCandidatoConDetalles(long IdCandidato)
        {
            try
            {
                var result = await _candidatoInterface.GetCandidatoConDetalle(IdCandidato);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar el candidato seleccionado"));
            }
        }


        [HttpDelete]
        [Route("EliminarCandidato")]
        public async Task<ActionResult> EliminarMotivo(long IdCandidato)
        {
            try
            {
                var resultDelete = await _candidatoInterface.Desactive(IdCandidato);
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
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar los candidatos"));
            }
        }

        [HttpPut]
        [Route("ActualizarCandidatos")]
        public async Task<ActionResult> ActualizarCandidatos(CandidatoDTO candidato)
        {
            try
            {
                var resultSave = await _candidatoInterface.Edit(candidato);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al actualizar los candidatos"));
            }
        }
    }
}
