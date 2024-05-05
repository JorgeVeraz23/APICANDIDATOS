using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PuntoDeVentaData.Entities.Security;
using Data;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Dto.SecurityDTO;
using Data.Interfaces.SecurityInterfaces;

namespace LinkQuality.Data.Repository.SecurityRepository
{
    public class RolRepository : IRolRepository
    {
        private readonly IUserStore<ApplicationUser> _userStore;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private MessageInfoDTO infoDTO = new();
        private readonly string _nombreController;
        public RolRepository(IUserStore<ApplicationUser> store, ApplicationDbContext context) //, RoleManager<IdentityRole> roleManager
        {
            _context = context;
            _userStore = store;
            //_roleManager = roleManager;
            _nombreController = "RolRepository";
        }
        public async Task<List<ItemRolDTO>> GetAllRoles()
        {
            try
            {
                List<ItemRolDTO> roles = await _context.ApplicationRoles
                    .Where(r => r.Active == true)
                    .Select(r => new ItemRolDTO
                    {
                        Id = r.Id,
                        RolName = r.Name,

                    }).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DropDownDTO>> GetAllRolesDrop()
        {
            try
            {
                List<DropDownDTO> roles = await _context.ApplicationRoles
                    .Where(r => r.Active == true)
                    .Select(r => new DropDownDTO
                    {
                        Key = r.Name ?? string.Empty,
                    }).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<MessageInfoDTO> EliminarRol(ObjetoEliminacionStringDTO rol)
        {
            try
            {
                var Rol = await _context.ApplicationRoles
                    .Where(r => r.Active && r.Id == rol.Id)
                    .FirstOrDefaultAsync();

                if (Rol != null)
                {
                    Rol.Active = false;
                    Rol.UserDelete = rol.UsuarioEliminacion;
                    Rol.IpDelete = rol.IpEliminacion;

                    Rol.DateDelete = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return infoDTO.AccionCompletada("El rol se elimino correctamente.");
                }
                return infoDTO.AccionFallida("No se encontro el rol", 404);
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nombreController, "Error al eliminar Rol");
            }
        }

        public async Task<MessageInfoDTO> RegistrarRol(ApplicationRole rol)
        {
            try
            {
                rol.Id = Guid.NewGuid().ToString();
                rol.Active = true;
                rol.ConcurrencyStamp = Guid.NewGuid().ToString();
                rol.NormalizedName = rol.Name?.ToUpper();

                rol.DateRegister = DateTime.Now;
                await _context.AddAsync(rol);
                await _context.SaveChangesAsync();

                infoDTO.AccionCompletada("Se ha agrego un nuevo rol");

                infoDTO.Detail = new { IdRol = rol.Id };
                return infoDTO;

            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nombreController, "Error al insertar rol.");
            }

        }

        public async Task<MessageInfoDTO> ModificarRol(RolDTO rolDTO)
        {
            try
            {
                var Rol = await _context.ApplicationRoles
                    .Where(r => r.Active && r.Id == rolDTO.Id)
                    .FirstOrDefaultAsync();

                if (Rol != null)
                {
                    Rol.Name = rolDTO.RolName;
                    Rol.NormalizedName = rolDTO.RolName.ToUpper();
                    Rol.UserModification = rolDTO.nameUsuario;
                    Rol.IpModification = rolDTO.ipUsuario;

                    Rol.DateModification = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return infoDTO.AccionCompletada("El nombre del rol se modifico correctamente.");
                }
                return infoDTO.AccionFallida("No se encontro el rol", 404);
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nombreController, "Error al modificar Rol");
            }
        }
    }
}
