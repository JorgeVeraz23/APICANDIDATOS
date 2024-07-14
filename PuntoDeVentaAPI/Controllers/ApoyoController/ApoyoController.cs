using AutoMapper;
using Data;
using Data.Interfaces.ApiCandidatosInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;

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



    }
    }
