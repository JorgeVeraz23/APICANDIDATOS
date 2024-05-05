using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces.SecurityInterfaces;
using Data;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using PuntoDeVentaData.Dto.SecurityDTO;

namespace LinkQuality.Data.Repository.SecurityRepository
{
    public class MenuRoleRepository : IMenuRolRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private IUnitOfWorkRepository _unit;
        private MessageInfoDTO infoDTO = new();
        private readonly string _nameRepository;
        public MenuRoleRepository(ApplicationDbContext context, IServiceProvider serviceProvider, IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;
            _nameRepository = "MenuRolRepository";
        }
        //public async Task<List<MenuRol>> GetMenuRol(string IdRol) => await _context.MenuRol.Where(p => p.IdRol == IdRol).ToListAsync();
        public async Task<List<MenuRolConsultaDto>> GetMenuPorRoleCore(IQueryable<MenuRole> query)
        {
            return await query.Select(p => new MenuRolConsultaDto
            {

                IdMenuRol = p.IdMenuRole,
                IdMenu = p.IdMenu ?? -1,
                IdMenuPadre = p.Menu.IdMenuFather,
                IdRol = p.IdRole,
                Nivel = p.Menu.Nivel,
                Orden = p.Menu.Orden,
                Codigo = p.Menu.Code ?? "",
                Descripcion = p.Menu.Description ?? "",
                Controlador = p.Menu.Controller ?? "",
                Vista = p.Menu.View ?? "",
                UrlAbsoluta = p.Menu.AbsoluteURL ?? "",
                Icon = p.Menu.Icon ?? "",
                ColorRef = p.Menu.ColorRef ?? "",
                IsVisible = p.IsVisible,
                IsCreate = p.IsCreate,
                IsEdit = p.IsEdit,
                IsPrint = p.IsPrint,
                IsProcess = p.IsProcess,
                IsSendEmail = p.IsSendEmail,
                IsDownloadPDF = p.IsDownloadPDF,
                IsDownloadExcel = p.IsDownloadExcel,
                Nombre = p.Menu.Name
            }).ToListAsync();
        }
        public async Task<List<MenuRolConsultaDto>> GetMenuPorRol(string IdRol)
        {

            var query = _context.MenuRoles
                .Where(p => p.IdRole == IdRol)
                .OrderBy(p => p.Menu.Code);
            var menuRol = await GetMenuPorRoleCore(query);
            return menuRol;
        }
        public async Task<List<MenuRolConsultaDto>> GetMenuByRoleNameDTO(string roleName)
        {
            //inner join to include in the future
            var query = _context.MenuRoles
                .Where(p => p.Role.Name == roleName)
                .OrderBy(p => p.Menu.Code);
            var menuRol = await GetMenuPorRoleCore(query);
            return menuRol;
        }


        public async Task<List<MainMenuRoleDTO>> GetMenuByRoleName(string roleName)
        {
            const int ROOT_LEVEL = 1;
            List<MenuRolConsultaDto> menu = await GetMenuByRoleNameDTO(roleName);
            return menu
                .Where(item => item.Nivel == ROOT_LEVEL && item.IdMenuPadre is null)
                .Select(item => new MainMenuRoleDTO
                {
                    IdMenuRole = item.IdMenuRol,
                    IdentityRole = item.IdentityRole,
                    Icon = item.Icon,
                    Name = item.Nombre,
                    Controller = item.Controlador,
                    View = item.Vista is not null ? "/" + item.Vista : item.Vista,
                    PermissionsMenuRole =
                        new PermissionsMenuRoleDTO
                        {
                            IsVisible = item.IsVisible,
                            IsCreate = item.IsCreate,
                            IsDownloadExcel = item.IsDownloadExcel,
                            IsDownloadPDF = item.IsDownloadPDF,
                            IsEdit = item.IsEdit,
                            IsPrint = item.IsPrint,
                            IsProcess = item.IsProcess,
                            IsSendEmail = item.IsSendEmail,
                        },
                    SubItems = menu
                        .Where(subItem => subItem.IdMenuPadre == item.IdMenu && subItem.Nivel == (ROOT_LEVEL + 1))
                        .OrderBy(subItem => subItem.Orden)
                        .Select(subItem => new SubMenuRoleDTO
                        {
                            IdMenuRole = subItem.IdMenuRol,
                            IdentityRole = subItem.IdentityRole,
                            Icon = subItem.Icon,
                            Name = subItem.Nombre,
                            Controller = subItem.Controlador,
                            View = subItem.Vista is not null ? "/" + subItem.Vista : subItem.Vista,
                            PermissionsMenuRole =
                                new PermissionsMenuRoleDTO
                                {
                                    IsVisible = subItem.IsVisible,
                                    IsCreate = subItem.IsCreate,
                                    IsDownloadExcel = subItem.IsDownloadExcel,
                                    IsDownloadPDF = subItem.IsDownloadPDF,
                                    IsEdit = subItem.IsEdit,
                                    IsPrint = subItem.IsPrint,
                                    IsProcess = subItem.IsProcess,
                                    IsSendEmail = subItem.IsSendEmail,
                                },
                            SubItems = menu
                            .Where(subItemThird => subItemThird.IdMenuPadre == subItem.IdMenu && subItemThird.Nivel == (ROOT_LEVEL + 2))
                            .OrderBy(subItemThird => subItemThird.Orden)
                            .Select(subItemThird => new SubMenuRoleDTO
                            {
                                IdMenuRole = subItemThird.IdMenuRol,
                                IdentityRole = subItemThird.IdentityRole,
                                Icon = subItemThird.Icon,
                                Name = subItemThird.Nombre,
                                Controller = subItemThird.Controlador,
                                View = subItemThird.Vista is not null ? "/" + subItemThird.Vista : subItemThird.Vista,
                                PermissionsMenuRole =
                                    new PermissionsMenuRoleDTO
                                    {
                                        IsVisible = subItemThird.IsVisible,
                                        IsCreate = subItemThird.IsCreate,
                                        IsDownloadExcel = subItemThird.IsDownloadExcel,
                                        IsDownloadPDF = subItemThird.IsDownloadPDF,
                                        IsEdit = subItemThird.IsEdit,
                                        IsPrint = subItemThird.IsPrint,
                                        IsProcess = subItemThird.IsProcess,
                                        IsSendEmail = subItemThird.IsSendEmail,
                                    },


                            })
                            .ToList()

                        })
                        .ToList()
                }).ToList();
        }
        public async Task<MessageInfoDTO> RegistrarMenuRol(MenuRole menuRol)
        {
            try
            {
                menuRol.Active = true;
                menuRol.DateRegister = DateTime.Now;
                await _context.AddAsync(menuRol);

                await _unit.SaveChangesAsync();
                infoDTO.AccionCompletada("Se registro correctamente el menu rol.");

                infoDTO.Detail = new { idMenuRol = menuRol.IdMenuRole };
                return infoDTO;
            }
            catch (Exception ex)
            {
                return (infoDTO.ErrorInterno(ex, _nameRepository, "Error al registrar un Meno por rol"));
            }

        }

        public async Task<MessageInfoDTO> ModificarMenuRol(MenuRole _menuRol)
        {
            try
            {
                var menuRol = await _context.MenuRoles.FirstOrDefaultAsync(ml => ml.IdMenuRole == _menuRol.IdMenuRole);

                if (menuRol == null)
                {
                    return infoDTO.AccionFallida("No se encontro el Menu por Rol.", 404);
                }
                menuRol.IdRole = _menuRol.IdRole;
                menuRol.IdMenu = _menuRol.IdMenu;
                menuRol.IsVisible = _menuRol.IsVisible;
                menuRol.IsCreate = _menuRol.IsCreate;
                menuRol.IsProcess = _menuRol.IsProcess;
                menuRol.IsPrint = _menuRol.IsPrint;
                menuRol.IsSendEmail = _menuRol.IsSendEmail;
                menuRol.IsDownloadExcel = _menuRol.IsDownloadExcel;
                menuRol.IsDownloadPDF = _menuRol.IsDownloadPDF;

                menuRol.DateModification = DateTime.Now;
                menuRol.UserModification = _menuRol.UserModification;
                menuRol.IpModification = _menuRol.IpModification;
                await _unit.SaveChangesAsync();

                return infoDTO.AccionCompletada("El menu por rol se modifico correctamente.");
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nameRepository, "Error al modificar el menu por rol,");
            }

        }
        public async Task<MessageInfoDTO> EliminarMenuRole(ObjetoEliminacionDTO menuRolEliminacion)
        {
            var menu = await _context.MenuRoles.FindAsync(menuRolEliminacion.Id);

            if (menu == null)
            {
                return infoDTO.AccionFallida("No se encontro el Menu por Rol.", 404);
            }

            menu.Active = false;
            menu.DateDelete = DateTime.Now;
            menu.UserDelete = menuRolEliminacion.UsuarioEliminacion;
            menu.IpDelete = menuRolEliminacion.IpEliminacion;
            await _unit.SaveChangesAsync();

            return infoDTO.AccionCompletada("Se agrego correctamente el menu por rol correctamente.");
        }

      
    }
}
