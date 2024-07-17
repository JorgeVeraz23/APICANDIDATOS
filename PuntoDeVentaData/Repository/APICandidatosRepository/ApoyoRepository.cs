using Data.Dto.ApoyoDTO;
using Data.Entities.Apoyo;
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
    public class ApoyoRepository : ApoyoInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;

        public ApoyoRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;

            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _username = Task.Run(async () => 
            (
                await userManager.FindByNameAsync(
                    httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? "")
            )?.UserName ?? "Desconocido").Result;

        }

        public async Task<List<MostrarApoyoDTO>> GetAll()
        {
            var apoyo = await _context.Apoyos.Where(x => x.Active).Select(c => new MostrarApoyoDTO
            {
                IdApoyo = c.IdApoyo,
                NombreDelPartidario = c.NombreDelPartidario,
                NombreCandidato = c.Candidato.NombreCandidato,
                Descripcion = c.Descripcion,
            }).ToListAsync();

            return apoyo;
        }

        public async Task<ApoyoDTO> GetApoyo(long id)
        {
            var apoyoSelected = await _context.Apoyos.Where(x => x.Active && x.IdApoyo == id).Select(c => new ApoyoDTO
            {
                IdApoyo = c.IdApoyo,
                NombreDelPartidario = c.NombreDelPartidario,
                IdCandidato = c.IdCandidato,
                Descripcion = c.Descripcion,
            }).FirstOrDefaultAsync();

            return apoyoSelected;
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var apoyoToDelete = await _context.Apoyos.Where(x => x.Active && x.IdApoyo == id).FirstOrDefaultAsync();

            if( apoyoToDelete != null )
            {
                infoDTO.AccionFallida("El apoyo seleccionado no se encuentra disponible", 400);
            }

            apoyoToDelete.DateDelete = DateTime.Now;
            apoyoToDelete.Active = false;
            apoyoToDelete.UserDelete = _username;
            apoyoToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("El apoyo seleccionado a sido eliminado correctamente");

            return infoDTO;

        }

        public async Task<MessageInfoDTO> Create(ApoyoDTO data)
        {
            var isAlreadyExist = await _context.Apoyos.Where(x => x.Active && x.NombreDelPartidario.ToUpper().Equals(data.NombreDelPartidario.ToUpper())).AnyAsync();

            if( isAlreadyExist )
            {
                infoDTO.AccionFallida("Ya existe un Apoyo registrado con ese nombre", 400);
                return infoDTO;
            }

            Apoyo apoyo = new Apoyo();
            apoyo.Active = true;
            apoyo.NombreDelPartidario = data.NombreDelPartidario;
            apoyo.IdCandidato = data.IdCandidato;
            apoyo.UserRegister = _username;
            apoyo.IpRegister = _ip;

            await _context.Apoyos.AddAsync(apoyo);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado el apoyo");

            return infoDTO;


        }

        public async Task<MessageInfoDTO> Edit(ApoyoDTO data)
        {
            try
            {
                var model = await _context.Apoyos.Where(x => x.Active && x.IdApoyo == data.IdApoyo).FirstOrDefaultAsync() ?? throw new Exception("No se encontro el apoyo");

                model.NombreDelPartidario = data.NombreDelPartidario;
                model.IdCandidato = data.IdCandidato;
                model.Descripcion = data.Descripcion;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }catch(Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "ApoyoRepository", "Error al intentar editar el apoyo seleccionado");
            }
        }
    }
}
