using Data.Dto.ExperienciaDTO;
using Data.Dto.PartidoPoliticoDTO;
using Data.Entities.Transpariencia;
using Data.Entities.PartidosPoliticos;
using Data.Entities.Trayectoria;
using Data.Interfaces.ApiCandidatosInterfaces;
using Data.Interfaces.SecurityInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.APICandidatosRepository
{
    public class TrayectoriaRepository : TrayectoriaInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public TrayectoriaRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;

            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _username = Task.Run(async () =>
            (
                await userManager.FindByNameAsync(
                    httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""
                )
            )?.UserName ?? "Desconocido").Result;
        }

        public async Task<MessageInfoDTO> Create(TrayectoriaDTO data)
        {
            var isAlreadyExist = await _context.Trayectorias.Where(x => x.Active && x.Titulo.ToUpper().Equals(data.Titulo.ToUpper())).AnyAsync();

            if (isAlreadyExist)
            {
                infoDTO.AccionFallida("Ya existe una trayectoria registrado con ese nombre", 400);
                return infoDTO;
            }

            Trayectoria trayectoria = new Trayectoria();
            trayectoria.Active = true;
            trayectoria.Titulo = data.Titulo;
            trayectoria.Descripcion = data.Descripcion;
            trayectoria.IdCandidato = data.IdCandidato;
            trayectoria.DateRegister = DateTime.Now;
            trayectoria.UserRegister = _username;
            trayectoria.IpRegister = _ip;

            await _context.Trayectorias.AddAsync(trayectoria);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado la trayectoria");
            return infoDTO;
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {

            var trayectoriaToDelete = await _context.Trayectorias.Where(x => x.Active && x.IdTrayectoria == id).FirstOrDefaultAsync();
            if (trayectoriaToDelete != null)
            {
                infoDTO.AccionFallida("La trayectoria seleccionada no se encuentra disponible", 400);
            }
            trayectoriaToDelete.DateDelete = DateTime.Now;
            trayectoriaToDelete.Active = false;
            trayectoriaToDelete.UserDelete = _username;
            trayectoriaToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("La trayectoria seleccionada a sido eliminada correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(TrayectoriaDTO data)
        {
            try
            {
                var model = await _context.Trayectorias.Where(x => x.Active && x.IdTrayectoria == data.IdTrayectoria).FirstOrDefaultAsync() ?? throw new Exception("No se encontro la trayectoria");

                model.Titulo = data.Titulo;
                model.Descripcion = data.Descripcion;
                model.IdCandidato = data.IdCandidato;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "TrayectoriaRepository", "Error al intentar actualizar la trayectoria");
            }
        }

        public async Task<TrayectoriaDTO> Get(long id)
        {

            var trayectoriaSelected = await _context.Trayectorias.Where(x => x.Active && x.IdTrayectoria == id).Select(c => new TrayectoriaDTO
            {
                IdTrayectoria = c.IdTrayectoria,
                Titulo = c.Titulo,
                Descripcion = c.Descripcion,
                IdCandidato = c.IdCandidato,
            }
             ).FirstOrDefaultAsync();
            return trayectoriaSelected;
        }


        public async Task<List<MostrarTrayectoriaDTO>> GetAll()
        {
            var trayectoria = await _context.Trayectorias.Where(x => x.Active).Select(c => new MostrarTrayectoriaDTO
            {
                IdTrayectoria = c.IdTrayectoria,
                Titulo = c.Titulo,
                Descripcion = c.Descripcion,
                NombreCandidato = c.Candidato.NombreCandidato,

            }).ToListAsync();
            return trayectoria;
        }
    }
}
