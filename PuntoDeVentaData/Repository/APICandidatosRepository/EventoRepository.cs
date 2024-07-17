using Data.Dto.EventoDTO;
using Data.Entities.Cargo;
using Data.Entities.Evento;
using Data.Interfaces.ApiCandidatosInterfaces;
using Data.Interfaces.SecurityInterfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class EventoRepository : EventoInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;

        public EventoRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository, IHttpContextAccessor httpContextAccessor)
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

        public async  Task<MessageInfoDTO> Create(EventoDTO data)
        {

            Evento evento = new Evento();
            evento.Active = true;
            evento.Titulo = data.Titulo;
            evento.Fecha = data.Fecha;
            evento.Ubicacion = data.Ubicacion;
            evento.IdCandidato = data.IdCandidato;
            evento.DateRegister = DateTime.Now;
            evento.UserRegister = _username;
            evento.IpRegister = _ip;

            await _context.Eventos.AddAsync(evento);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado el cargo");
            return infoDTO;
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var eventoToDelete = await _context.Eventos.Where(x => x.Active && x.IdEvento == id).FirstOrDefaultAsync();
            if (eventoToDelete != null)
            {
                infoDTO.AccionFallida("El evento seleccionado no se encuentra disponible", 400);
            }
            eventoToDelete.DateDelete = DateTime.Now;
            eventoToDelete.Active = false;
            eventoToDelete.UserDelete = _username;
            eventoToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("El evento seleccionado a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(EventoDTO data)
        {

            try
            {
                var model = await _context.Eventos.Where(x => x.Active && x.IdEvento == data.IdEvento).FirstOrDefaultAsync() ?? throw new Exception("No se encontro el evento");

                model.Titulo = data.Titulo;
                model.Fecha = data.Fecha;
                model.Ubicacion = data.Ubicacion;
                model.IdCandidato = data.IdCandidato;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "EventoRepository", "Error al intentar actualizar el evento");
            }
        }

        public async Task<EventoDTO> Get(long id)
        {
            var eventoSelected = await _context.Eventos.Where(x => x.Active && x.IdEvento == id).Select(c => new EventoDTO
            {
                Titulo = c.Titulo,
                Fecha = c.Fecha,
                Ubicacion = c.Ubicacion,
                IdCandidato = c.IdCandidato,
            }
               ).FirstOrDefaultAsync();
            return eventoSelected;
        }

        public async Task<List<MostrarEventoDto>> GetAll()
        {
            var eventos = await _context.Eventos.Where(x => x.Active).Select(c => new MostrarEventoDto
            {
                Titulo = c.Titulo,
                Fecha = c.Fecha,
                Ubicacion = c.Ubicacion,
                NombreCandidato = c.Candidato.NombreCandidato,

            }).ToListAsync();
            return eventos;
        }
    }
}
