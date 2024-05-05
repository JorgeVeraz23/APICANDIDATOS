using Data;
using Data.Dto.GenericDTO;
using Data.Interfaces.UserInterfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;


namespace LinkQuality.Data.Repository.UserRepository
{
    public class UserRepository : UserInterface
    {
        private readonly ApplicationDbContext _context;

        private readonly string _username;
        private readonly string _ip;

        public UserRepository(ApplicationDbContext ctx, UserManager<ApplicationUser> uManager, IHttpContextAccessor httpCtxAccessor)
        {
            _context = ctx;

            _ip = httpCtxAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _username = Task.Run(async () =>
            (
                await uManager.FindByNameAsync(
                    httpCtxAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""
                )
            )?.UserName ?? "Desconocido").Result;
        }

        public async Task<List<GenericListDTO>> GetInspectorList()
        {
            var inspectorRole = await _context.Roles.AsNoTracking()
                .Where(x => x.NormalizedName == "INSPECTOR")
                .Select(x => new { x.Id })
                .FirstOrDefaultAsync();

            if (inspectorRole == null)
            {
                return [];
            }

            var inspectorList = await _context.UserRoles.AsNoTracking()
                .Where(x => x.RoleId == inspectorRole.Id)
                .Join(
                    _context.Users.AsNoTracking(),
                    result => result.UserId,
                    user => user.Id,
                    (result, user) => new GenericListDTO
                    { 
                        Value = user.Id,
                        Text = $"{user.FirstName} {user.LastName}",
                        Active = user.Activo ?? false,
                        Username = user.UserName

                    }
                ).ToListAsync() ?? [];

            return inspectorList;
        }


        public async Task<List<GenericListDTO>> GetActivesInspectorList()
        {
            var inspectorRole = await _context.Roles.AsNoTracking()
                .Where(x => x.NormalizedName == "INSPECTOR")
                .Select(x => new { x.Id })
                .FirstOrDefaultAsync();

            if (inspectorRole == null)
            {
                return [];
            }

            var inspectorList = await _context.UserRoles.AsNoTracking()
                .Where(x => x.RoleId == inspectorRole.Id)
                .Join(
                    _context.Users.AsNoTracking().Where(u => u.Activo.HasValue && u.Activo.Value),
                    result => result.UserId,
                    user => user.Id,
                    (result, user) => new GenericListDTO
                    {
                        Value = user.Id,
                        Text = $"{user.FirstName} {user.LastName}",
                        Active = user.Activo ?? false,
                        Username = user.UserName,
                    }
                ).ToListAsync() ?? [];

            return inspectorList;
        }

        public async Task<MessageInfoDTO> ActiveDesactiveInspectorSelected(string UserName)
        {
            MessageInfoDTO messageInfoDTO = new MessageInfoDTO();
            var inspectorRole = await _context.Roles.AsNoTracking()
                .Where(x => x.NormalizedName == "INSPECTOR")
                .Select(x => new { x.Id })
                .FirstOrDefaultAsync();

            if (inspectorRole == null)
            {
                messageInfoDTO.AccionFallida("No se encuentra configurado el perfil de inspector",400);
                return messageInfoDTO;
            }

            var inspectorSelected = await _context.Users.AsNoTracking().Where(i => i.UserName == UserName).FirstOrDefaultAsync();

            if(inspectorSelected == null)
            {
                messageInfoDTO.AccionFallida("El usuario seleccionado no se encuentra disponible", 400);
                return messageInfoDTO;
            }

            //veo si es inspector
            var isInspector = await _context.UserRoles.Where(r => r.RoleId == inspectorRole.Id && r.UserId == inspectorSelected.Id).AnyAsync();
            if (isInspector)
            {
                //mismo metodo para activar o desactivar
                if (inspectorSelected.Activo == false)
                {
                    inspectorSelected.Activo = true;
                    inspectorSelected.FechaModificacion = DateTime.Now;
                    inspectorSelected.UsuarioModificacion = _username;
                    inspectorSelected.IpModificacion = _ip;
                    messageInfoDTO.AccionCompletada("Inspector activado correctamente");

                }
                else
                {
                    inspectorSelected.Activo = false;
                    inspectorSelected.FechaEliminacion = DateTime.Now;
                    inspectorSelected.UsuarioEliminacion = _username;
                    inspectorSelected.IpEliminacion = _ip;
                    messageInfoDTO.AccionCompletada("Inspector desactivado correctamente");
                }


                await _context.SaveChangesAsync();
            }
            else
            {
                messageInfoDTO.AccionFallida("El usuario seleccionado no se encuentra registrado como inspector", 400);

            }

            return messageInfoDTO;
                
        }

        public async Task<MessageInfoDTO> ValidateAppVersion(string appVersion)
        {
            MessageInfoDTO messageInfoDTO = new MessageInfoDTO();
            var versionActive = await _context.ApplicationVersions.Where(x => x.Active && x.VersionName.Equals(appVersion)).FirstOrDefaultAsync();
            if(versionActive == null)
            {
                messageInfoDTO.AccionFallida("Se encuentra utilizando una versión antigua. Por favor, actualizar a la última versión disponible en tiendas",400);
            }
            else
            {
                messageInfoDTO.AccionCompletada("Se encuentra utilizando la versión más actual del app");
            }

            return messageInfoDTO;
        }

        
    }
}
