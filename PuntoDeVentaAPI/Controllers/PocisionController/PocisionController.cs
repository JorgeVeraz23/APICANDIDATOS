using AutoMapper;
using Data.Interfaces.ApiCandidatosInterfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Data.Dto.EventoDTO;
using System.Net;
using Data.Dto.PocisionDTO;

namespace PuntoDeVentaAPI.Controllers.PocisionController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PocisionController : ControllerBase
    {


        private readonly PocisionInterface _pocisionInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Logger _log = LogManager.GetLogger("PocisionController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        public readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;

        public PocisionController(PocisionInterface pocisionInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _pocisionInterface = pocisionInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "PocisionController";
            _httpContextAccessor = httpContextAccessor;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }

        [HttpGet]
        [Route("GetAllPocision")]
        public async Task<ActionResult> GetAllPocision()
        {
            try
            {
                var result = await _pocisionInterface.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los partidos"));
            }
        }

        [HttpPost]
        [Route("CrearPocision")]
        public async Task<ActionResult> CrearPocision(PocisionDTO pocision)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                var resultSave = await _pocisionInterface.Create(pocision);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al crear las pocisones"));
            }
        }

        [HttpGet]
        [Route("GetPocisiones")]
        public async Task<ActionResult> GetPocisiones(long IdPocision)
        {
            try
            {
                var result = await _pocisionInterface.Get(IdPocision);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al consultar la pocision seleccionada"));
            }
        }


        [HttpDelete]
        [Route("EliminarPocision")]
        public async Task<ActionResult> EliminarPocision(long IdPocision)
        {
            try
            {
                var resultDelete = await _pocisionInterface.Desactive(IdPocision);
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
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar la pocision"));
            }
        }

        [HttpPut]
        [Route("ActualizarPocision")]
        public async Task<ActionResult> ActualizarPocision(PocisionDTO pocision)
        {
            try
            {
                var resultSave = await _pocisionInterface.Edit(pocision);
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
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al actualizar la pocision"));
            }
        }

    }
}
