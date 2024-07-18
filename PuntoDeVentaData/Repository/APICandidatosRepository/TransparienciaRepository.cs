using Data.Dto.PartidoPoliticoDTO;
using Data.Dto.RedSocialDTO;
using Data.Dto.TransparienciaDTO;
using Data.Entities.PartidosPoliticos;
using Data.Entities.Transpariencia;
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
    public class TransparienciaRepository : TransparienciaInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public TransparienciaRepository(
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

        public async Task<MessageInfoDTO> Create(TransparienciaDTO data)
        {

            Transpariencia transpariencia = new Transpariencia();
            transpariencia.Active = true;
            transpariencia.DeclaracionesDeBienes = data.DeclaracionesDeBienes;
            transpariencia.InvolucradoEnEscandalos = data.InvolucradoEnEscandalos;
            transpariencia.EvaluacionesDeEtica = data.EvaluacionesDeEtica;
            transpariencia.DateRegister = DateTime.Now;
            transpariencia.UserRegister = _username;
            transpariencia.IpRegister = _ip;

            await _context.Transpariencias.AddAsync(transpariencia);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado la transparencia");
            return infoDTO;
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var transparenciaToDelete = await _context.Transpariencias.Where(x => x.Active && x.IdTranspariencia == id).FirstOrDefaultAsync();
            if (transparenciaToDelete != null)
            {
                infoDTO.AccionFallida("La transparencia seleccionado no se encuentra disponible", 400);
            }
            transparenciaToDelete.DateDelete = DateTime.Now;
            transparenciaToDelete.Active = false;
            transparenciaToDelete.UserDelete = _username;
            transparenciaToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("La transparencia seleccionada a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(TransparienciaDTO data)
        {
            try
            {
                var model = await _context.Transpariencias.Where(x => x.Active && x.IdTranspariencia == data.IdTranspariencia).FirstOrDefaultAsync() ?? throw new Exception("No se encontro la transparecia");

                model.DeclaracionesDeBienes = data.DeclaracionesDeBienes;
                model.InvolucradoEnEscandalos = data.InvolucradoEnEscandalos;
                model.EvaluacionesDeEtica = data.EvaluacionesDeEtica;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "TransparienciaRepository", "Error al intentar actualizar la transparencia");
            }
        }

        public async Task<TransparienciaDTO> Get(long id)
        {
            var transparenciaSelected = await _context.Transpariencias.Where(x => x.Active && x.IdTranspariencia == id).Select(c => new TransparienciaDTO
            {
                IdTranspariencia = c.IdTranspariencia,
                DeclaracionesDeBienes = c.DeclaracionesDeBienes,
                InvolucradoEnEscandalos = c.InvolucradoEnEscandalos,
                EvaluacionesDeEtica = c.EvaluacionesDeEtica,
            }
           ).FirstOrDefaultAsync();
            return transparenciaSelected;
        }

        public async Task<List<MostrarTransparienciaDTO>> GetAll()
        {
            var transpariencia = await _context.Transpariencias.Where(x => x.Active).Select(c => new MostrarTransparienciaDTO
            {
                IdTranspariencia = c.IdTranspariencia,
                DeclaracionesDeBienes = c.DeclaracionesDeBienes,
                InvolucradoEnEscandalos = c.InvolucradoEnEscandalos,
                EvaluacionesDeEtica = c.EvaluacionesDeEtica,
            }).ToListAsync();
            return transpariencia;
        }


    }
}
