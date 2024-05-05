using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Data;
using Data.Interfaces.SecurityInterfaces;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;

namespace LinkQuality.Data.Repository.SecurityRepository
{
    public class MenuRepository : IMenuRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly string _nameRepository;
        private IUnitOfWorkRepository _unit;
        private MessageInfoDTO infoDTO = new();



        public MenuRepository(ApplicationDbContext context, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;
            _nameRepository = "MenuRepository";
        }
        public async Task<List<Menu>> GetMenu() => await _context.Menus.Where(m => m.Active).ToListAsync();

        public async Task<MessageInfoDTO> RegistrarMenu(Menu menu)
        {

            try
            {
                menu.IdMenu = 0;
                var codigo = string.Empty;
                var codigocompleto = string.Empty;
                switch (menu.Nivel)
                {
                    case 1:
                        menu.Code = codigo;
                        break;
                    case 2:
                        codigo = menu.Orden.ToString().PadLeft(3, '0');
                        codigocompleto = _context.Menus.FirstOrDefault(p => p.IdMenu == menu.IdMenuFather)?.Code ?? "";
                        menu.Code = $"{codigocompleto}.{codigo}";
                        break;
                    case 3:
                        codigo = menu.Orden.ToString().PadLeft(3, '0');
                        codigocompleto = _context.Menus.FirstOrDefault(p => p.IdMenu == menu.IdMenuFather)?.Code ?? "";
                        menu.Code = $"{codigocompleto}.{codigo}";
                        break;
                }
                menu.Active = true;
                menu.DateRegister = DateTime.Now;
                await _context.AddAsync(menu);
                await _unit.SaveChangesAsync();

                infoDTO.AccionCompletada("Se registro correctamente el menu");
                var idMenu = await _context.Menus
                    .Where(m => m.Active && m.Name == menu.Name)
                    .Select(m => m.IdMenu)
                    .FirstOrDefaultAsync();
                infoDTO.Detail = new { IdMenu = idMenu };

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nameRepository, "Error al registrar un menu");
            }
        }

        public async Task<MessageInfoDTO> ModificarMenu(Menu _menu)
        {
            try
            {
                var menu = await _context.Menus.FirstOrDefaultAsync(m => m.IdMenu == _menu.IdMenu);

                if (menu == null)
                {
                    return infoDTO.AccionFallida("No se encontro el menu seleccionado", 404);
                }

                menu.IdMenuFather = _menu.IdMenuFather;
                menu.Nivel = _menu.Nivel;
                menu.Orden = _menu.Orden;
                var codigo = string.Empty;
                var codigocompleto = string.Empty;
                switch (_menu.Nivel)
                {
                    case 1:
                        menu.Code = codigo;
                        break;
                    case 2:
                        codigo = _menu.Orden.ToString().PadLeft(3, '0');
                        codigocompleto = _context.Menus.FirstOrDefault(p => p.IdMenu == _menu.IdMenuFather)?.Code ?? "";
                        menu.Code = $"{codigocompleto}.{codigo}";
                        break;
                    case 3:
                        codigo = _menu.Orden.ToString().PadLeft(3, '0');
                        codigocompleto = _context.Menus.FirstOrDefault(p => p.IdMenu == _menu.IdMenuFather)?.Code ?? "";
                        menu.Code = $"{codigocompleto}.{codigo}";
                        break;
                }

                menu.Name = _menu.Name;
                menu.Description = _menu.Description;
                menu.Controller = _menu.Controller;
                menu.View = _menu.View;
                menu.AbsoluteURL = _menu.AbsoluteURL;
                menu.RelativeURL = _menu.RelativeURL;
                menu.Icon = _menu.Icon;
                menu.ColorRef = _menu.ColorRef;
                menu.IsVisible = _menu.IsVisible;
                menu.IsCreate = _menu.IsCreate;
                menu.IsEdit = _menu.IsEdit;
                menu.IsSendEmail = _menu.IsSendEmail;
                menu.IsPrint = _menu.IsPrint;
                menu.IsDownloadExcel = _menu.IsDownloadExcel;
                menu.IsProcess = _menu.IsProcess;
                menu.IsApproved = _menu.IsApproved;

                menu.DateModification = DateTime.Now;
                menu.UserModification = _menu.UserModification;
                menu.IpModification = _menu.IpModification;
                await _unit.SaveChangesAsync();

                return infoDTO.AccionCompletada("Se modifico correctamente el menu");
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nameRepository, "Error al actualizar el menu");
            }
        }
        public async Task<MessageInfoDTO> EliminarMenu(ObjetoEliminacionDTO menuEliminacion)
        {

            try
            {
                var menu = await _context.Menus.FindAsync(menuEliminacion.Id);

                if (menu == null)
                {
                    return infoDTO.AccionFallida("No se encontro el menu.", 404);
                }

                menu.Active = false;
                menu.DateDelete = DateTime.Now;
                menu.UserDelete = menuEliminacion.UsuarioEliminacion;
                menu.IpDelete = menuEliminacion.IpEliminacion;
                await _unit.SaveChangesAsync();
                return infoDTO.AccionCompletada("Se elimino correctamente el menu");
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nameRepository, "Error al eliminar el menu.");
            }
        }
    }
}
