using Data.Dto.PartidoPoliticoDTO;
using Data.Dto.PropuestaDTO;
using Data.Entities.Pocision;
using Data.Entities.Propuestas;
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
    public class PropuestaRepository : PropuestaInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public PropuestaRepository(
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


        public async Task<MessageInfoDTO> Create(PropuestaDTO data)
        {
            var isAlreadyExist = await _context.Propuestas.Where(x => x.Active && x.Titulo.ToUpper().Equals(data.Titulo.ToUpper())).AnyAsync();

            if (isAlreadyExist)
            {
                infoDTO.AccionFallida("Ya existe una propuesta registrada con ese nombre", 400);
                return infoDTO;
            }

            Propuesta propuesta = new Propuesta();
            propuesta.Active = true;
            propuesta.Titulo = data.Titulo;
            propuesta.Descripción = data.Descripción;
            propuesta.IdCandidato = data.IdCandidato;
            propuesta.DateRegister = DateTime.Now;
            propuesta.UserRegister = _username;
            propuesta.IpRegister = _ip;

            await _context.Propuestas.AddAsync(propuesta);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado la propuesta");
            return infoDTO; new NotImplementedException();
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var propuestaToDelete = await _context.Propuestas.Where(x => x.Active && x.IdPropuesta == id).FirstOrDefaultAsync();
            if (propuestaToDelete != null)
            {
                infoDTO.AccionFallida("La propuesta seleccionado no se encuentra disponible", 400);
            }
            propuestaToDelete.DateDelete = DateTime.Now;
            propuestaToDelete.Active = false;
            propuestaToDelete.UserDelete = _username;
            propuestaToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("La propuesta seleccionada a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(PropuestaDTO data)
        {
            try
            {
                var model = await _context.Propuestas.Where(x => x.Active && x.IdPropuesta == data.IdPropuesta).FirstOrDefaultAsync() ?? throw new Exception("No se encontro la propuesta");

                model.Titulo = data.Titulo;
                model.Descripción = data.Descripción;
                model.Area = data.Area;
                model.IdCandidato = data.IdCandidato;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "PocisionRepository", "Error al intentar actualizar el partido");
            }
        }

        public async Task<PropuestaDTO> Get(long id)
        {
            var propuestaSelected = await _context.Propuestas.Where(x => x.Active && x.IdPropuesta == id).Select(c => new PropuestaDTO
            {
                IdPropuesta = c.IdPropuesta,
                Titulo = c.Titulo,
                Descripción = c.Descripción,
                Area = c.Area,
                IdCandidato = c.IdCandidato,
            }
             ).FirstOrDefaultAsync();
            return propuestaSelected;
        }

        public async Task<List<MostrarPropuestaDTO>> GetAll()
        {
            var partido = await _context.Propuestas.Where(x => x.Active).Select(c => new MostrarPropuestaDTO
            {
                IdPropuesta = c.IdPropuesta,
                Titulo = c.Titulo,
                Descripción = c.Descripción,
                Area = c.Area,
                NombreCandidato = c.Candidato.NombreCandidato,

            }).ToListAsync();
            return partido;
        }
    }
}
