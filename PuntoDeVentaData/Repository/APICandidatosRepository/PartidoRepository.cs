using Data.Dto.CargoDTO;
using Data.Dto.PartidoPoliticoDTO;
using Data.Dto.UtilitiesDTO;
using Data.Entities.Cargo;
using Data.Entities.PartidosPoliticos;
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
    public class PartidoRepository : PartidoInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public PartidoRepository(
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

        public async Task<MessageInfoDTO> Create(PartidoDTO data)
        {
            var isAlreadyExist = await _context.Partidos.Where(x => x.Active && x.NombrePartido.ToUpper().Equals(data.NombrePartido.ToUpper())).AnyAsync();

            if (isAlreadyExist)
            {
                infoDTO.AccionFallida("Ya existe un partido registrado con ese nombre", 400);
                return infoDTO;
            }

            Partido partido = new Partido();
            partido.Active = true;
            partido.NombrePartido = data.NombrePartido;
            partido.DateRegister = DateTime.Now;
            partido.UserRegister = _username;
            partido.IpRegister = _ip;

            await _context.Partidos.AddAsync(partido);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado el partido");
            return infoDTO;
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var partidoToDelete = await _context.Partidos.Where(x => x.Active && x.IdPartido == id).FirstOrDefaultAsync();
            if (partidoToDelete != null)
            {
                infoDTO.AccionFallida("El partido seleccionado no se encuentra disponible", 400);
            }
            partidoToDelete.DateDelete = DateTime.Now;
            partidoToDelete.Active = false;
            partidoToDelete.UserDelete = _username;
            partidoToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("El partido seleccionado a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(PartidoDTO data)
        {
            try
            {
                var model = await _context.Partidos.Where(x => x.Active && x.IdPartido == data.IdPartido).FirstOrDefaultAsync() ?? throw new Exception("No se encontro el partido");

                model.NombrePartido = data.NombrePartido;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "PartidoRepository", "Error al intentar actualizar el partido");
            }
        }

        public async Task<PartidoDTO> Get(long id)
        {
            var partidoSelected = await _context.Partidos.Where(x => x.Active && x.IdPartido == id).Select(c => new PartidoDTO
            {
                IdPartido = c.IdPartido,
                NombrePartido = c.NombrePartido,
            }
             ).FirstOrDefaultAsync();
            return partidoSelected;
        }

        public async Task<List<PartidoDTO>> GetAll()
        {
            var partido = await _context.Partidos.Where(x => x.Active).Select(c => new PartidoDTO
            {
                IdPartido = c.IdPartido,
                NombrePartido = c.NombrePartido,

            }).ToListAsync();
            return partido;
        }

        public async Task<List<KeyValueDTO>> KeyValuePartido()
        {
            var selectorPartido = await _context.Partidos.Where(x => x.Active).Select(c => new KeyValueDTO
            {
                Key = c.IdPartido,
                Value = c.NombrePartido
            }).ToListAsync();

            return selectorPartido;
        }
    }
}
