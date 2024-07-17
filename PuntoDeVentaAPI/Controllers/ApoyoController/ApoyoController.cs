using AutoMapper;
using Data;
using Data.Dto.ApoyoDTO;
using Data.Interfaces.ApiCandidatosInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System.Net;

namespace PuntoDeVentaAPI.Controllers.ApoyoController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApoyoController : ControllerBase
    {
        private readonly ApoyoInterface _apoyoInterface;
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private static Logger _log = LogManager.GetLogger("ApoyoController");
        MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _usuario;
        private readonly string _ip;
        private readonly string _nombreController;


        public ApoyoController(ApoyoInterface apoyoInterface, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, ApplicationUserManager userManager, IServiceProvider service, IMapper mapper, IConfiguration configuration)
        {
            _apoyoInterface = apoyoInterface;
            this._context = applicationDbContext;
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
            _nombreController = "ApoyoController";
            _httpContextAccesor = httpContextAccessor;
            _ip = _httpContextAccesor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }


        [HttpGet]
        [Route("GeAllApoyo")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _apoyoInterface.GetAll();
                return Ok(result);

            }catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los apoyos"));
            }
        }

        [HttpGet]
        [Route("GetApoyoById")]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                var result = await _apoyoInterface.GetApoyo(id);
                return Ok(result);
                
            }catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al conultar el apoyo seleccionado"));
            }
        }

        [HttpPost]
        [Route("CreateApoyo")]
        public async Task<ActionResult> CreateApoyo(ApoyoDTO apoyo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }

                var resultSave = await _apoyoInterface.Create(apoyo);
                if (resultSave.Success)
                {
                    return Ok(new MessageInfoDTO().AccionCompletada(resultSave.Message ?? string.Empty));
                }
                else
                {
                    return BadRequest(new MessageInfoDTO().AccionFallida(resultSave.Message ?? string.Empty, (int)HttpStatusCode.BadRequest));
                }
            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al crear los apoyos"));
            }
        }

        [HttpDelete]
        [Route("EliminarApoyo")]
        public async Task<ActionResult> EliminarApoyo(long idApoyo)
        {
            try
            {
                var resultDelete = await _apoyoInterface.Desactive(idApoyo);
                if (resultDelete.Success)
                {
                    return Ok(resultDelete.Success);
                }
                else
                {
                    return BadRequest(resultDelete.Message);
                }
            }catch(Exception ex) {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al eliminar los apoyos"));
            }

        }

        [HttpPut]
        [Route("ActualizarApoyo")]
        public async Task<ActionResult> ActualizarApoyos(ApoyoDTO apoyoDTO)
        {
            try
            {
                var resultSave = await _apoyoInterface.Edit(apoyoDTO);
                if (resultSave.Success)
                {
                    return Ok(resultSave.Success);
                }
                else
                {
                    return BadRequest(resultSave.Message);
                }
            }catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Errror al actualizar los apoyos"));
            }
        }


        



    }
    }
