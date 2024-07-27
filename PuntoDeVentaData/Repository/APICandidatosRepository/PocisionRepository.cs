using Data.Dto.PartidoPoliticoDTO;
using Data.Dto.PocisionDTO;
using Data.Entities.PartidosPoliticos;
using Data.Entities.Pocision;
using Data.Interfaces.ApiCandidatosInterfaces;
using Data.Interfaces.SecurityInterfaces;
using DinkToPdf;
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
    public class PocisionRepository : PocisionInterface
    {
        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public PocisionRepository(
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

        public async Task<MessageInfoDTO> Create(PocisionDTO data)
        {
            var isAlreadyExist = await _context.Posiciones.Where(x => x.Active && x.Tema.ToUpper().Equals(data.Tema.ToUpper())).AnyAsync();

            if (isAlreadyExist)
            {
                infoDTO.AccionFallida("Ya existe una pocision registrada con ese nombre", 400);
                return infoDTO;
            }

            Posición pocision = new Posición();
            pocision.Active = true;
            pocision.Tema = data.Tema;
            pocision.Postura = data.Postura;
            pocision.IdCandidato = data.IdCandidato;
            pocision.DateRegister = DateTime.Now;
            pocision.UserRegister = _username;
            pocision.IpRegister = _ip;

            await _context.Posiciones.AddAsync(pocision);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado la pocision");
            return infoDTO; new NotImplementedException();
        }

        public async  Task<MessageInfoDTO> Desactive(long id)
        {
            var pocisionToDelete = await _context.Posiciones.Where(x => x.Active && x.IdPocision == id).FirstOrDefaultAsync();
            if (pocisionToDelete != null)
            {
                infoDTO.AccionFallida("La pocision seleccionado no se encuentra disponible", 400);
            }
            pocisionToDelete.DateDelete = DateTime.Now;
            pocisionToDelete.Active = false;
            pocisionToDelete.UserDelete = _username;
            pocisionToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();
            infoDTO.AccionCompletada("La pocision seleccionada a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(PocisionDTO data)
        {
            try
            {
                var model = await _context.Posiciones.Where(x => x.Active && x.IdPocision == data.IdPocision).FirstOrDefaultAsync() ?? throw new Exception("No se encontro la pocision");

                model.Tema = data.Tema;
                model.Postura = data.Postura;
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

        public async Task<PocisionDTO> Get(long id)
        {
            var positionSelected = await _context.Posiciones.Where(x => x.Active && x.IdPocision == id).Select(c => new PocisionDTO
            {
                Tema = c.Tema,
                Postura = c.Postura,
                IdCandidato = c.IdCandidato,
            }
             ).FirstOrDefaultAsync();
            return positionSelected;
        }

        public async  Task<List<PocisionDTO>> GetAll()
        {
            var pocision = await _context.Posiciones.Where(x => x.Active).Select(c => new PocisionDTO
            {
                IdPocision = c.IdPocision,
                Postura = c.Postura,
                IdCandidato = c.IdCandidato,

            }).ToListAsync();
            return pocision;
        }
    }
}
