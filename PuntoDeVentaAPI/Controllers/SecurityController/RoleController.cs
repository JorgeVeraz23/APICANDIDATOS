using AutoMapper;
using Data.Interfaces.SecurityInterfaces;
using LinkQuality.Api.Services;

using Microsoft.AspNetCore.Mvc;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.SecurityDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;

namespace PuntoDeVentaAPI.Controllers.SecurityController
{
    [Route("api/Role")]
    public class RoleController : ControllerBase
    {
        private readonly IRolRepository _rol;
        private readonly IMapper _mapper;
        public readonly string _usuario;
        public readonly string _ip;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private MessageInfoDTO infoDTO;
        private readonly string _nombreController;

        public RoleController(IRolRepository rol, IMapper mapper, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _rol = rol;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _nombreController = "RoleController";
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            //_usuario = Task.Run(async () => await userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "username").Value)).Result.UserName;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
        }



        [HttpGet]
        [Route("GetList")]
        public async Task<ActionResult> GetLista()
        {
            try
            {
                var listaRoles = await _rol.GetAllRoles();


                return Ok(listaRoles);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los roles"));
            }

        }


        [HttpGet]
        [Route("GetAllRolesDrop")]
        public async Task<ActionResult> GetAllRolesDrop()
        {
            try
            {
                var listaRoles = await _rol.GetAllRolesDrop();


                return Ok(listaRoles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new MessageInfoDTO().ErrorInterno(ex, _nombreController, "Error al listar los roles"));
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddRole(string rolName)
        {
            ApplicationRole rol = new();
            rol.Name = rolName;
            rol.UserRegister = _usuario;
            rol.IpRegister = _ip;

            infoDTO = await _rol.RegistrarRol(rol);
            return StatusCode(infoDTO.Status, infoDTO);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateRol(RolDTO rolDTO)
        {
            rolDTO.ipUsuario = _ip;
            rolDTO.nameUsuario = _usuario;

            infoDTO = await _rol.ModificarRol(rolDTO);
            return StatusCode(infoDTO.Status, infoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRol(string id)
        {
            ObjetoEliminacionStringDTO rol = new();

            rol.Id = id;
            rol.IpEliminacion = _ip;
            rol.UsuarioEliminacion = _usuario;

            infoDTO = await _rol.EliminarRol(rol);
            return StatusCode(infoDTO.Status, infoDTO);
        }
    }


}
