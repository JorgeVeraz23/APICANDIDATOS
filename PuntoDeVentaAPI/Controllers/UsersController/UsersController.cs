using Amazon.S3.Model.Internal.MarshallTransformations;
using Data.Interfaces.UserInterfaces;
using LinkQuality.Api.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Dto.UtilitiesDTO;

namespace PuntoDeVentaAPI.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UserInterface  _userInterface;
        private readonly ApplicationUserManager _userManager;
        private readonly string _nameController = "UsersController";

        public UsersController(UserInterface userInterface, ApplicationUserManager userManager)
        {
            _userInterface = userInterface;
            _userManager = userManager;
        }

        [HttpGet("GetInspectorList")]
        public async Task<ActionResult> GetInspectorList()
        {
            try
            {
                var result = await _userInterface.GetInspectorList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nameController, "Error al listar los inspectores."));
            }
        }

        [HttpGet("GetActivesInspectorList")]
        public async Task<ActionResult> GetActivesInspectorList()
        {
            try
            {
                var result = await _userInterface.GetActivesInspectorList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nameController, "Error al listar los inspectores."));
            }
        }


        [HttpPut("ChangeStatusInspector")]
        public async Task<ActionResult> ChangeStatusInspector(string UserName)
        {
            try
            {
                
                var result = await _userManager.ActivateDesactivateInspectorSelected(UserName);
                return StatusCode(result.Status, result.Message);
            }catch (Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nameController, $"Error al eliminar inspector usuario {UserName}."));
            }
        }

        [HttpGet("ValidateVersionApp")]
        [AllowAnonymous]
        public async Task<ActionResult> ValidateVersionApp(string versionApp)
        {
            try
            {
                var result = await _userInterface.ValidateAppVersion(versionApp);
                if(result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }

            }catch(Exception ex)
            {
                return StatusCode(400, new MessageInfoDTO().ErrorInterno(ex, _nameController, $"Error al validar versión actual: {versionApp}."));
            }
        }
    }
}
