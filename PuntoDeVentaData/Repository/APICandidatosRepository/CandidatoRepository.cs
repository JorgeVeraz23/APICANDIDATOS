using Data.Dto.ApoyoDTO;
using Data.Dto.CandidatoDTO;
using Data.Dto.UtilitiesDTO;
using Data.Entities.Apoyo;
using Data.Entities.Candidatos;
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
    public class CandidatoRepository : CandidatoInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public CandidatoRepository(
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

        public async Task<MessageInfoDTO> Create(CandidatoDTO data)
        {
            var isAlreadyExist = await _context.Candidatos.Where(x => x.Active && x.NombreCandidato.ToUpper().Equals(data.NombreCandidato.ToUpper())).AnyAsync(); 
            
            if(isAlreadyExist)
            {
                infoDTO.AccionFallida("Ya existe un Candidato registrado con ese nombre", 400);
                return infoDTO;
            }

            Candidato candidato = new Candidato();
            candidato.Active = true;
            candidato.NombreCandidato = data.NombreCandidato;
            candidato.Edad = data.Edad;
            candidato.FotoUrl = data.FotoUrl;
            candidato.LugarDeNacimiento = data.LugarDeNacimiento;
            candidato.InformacionDeContacto = data.InformacionDeContacto;
            candidato.IdPartido = data.IdPartido;
            candidato.IdCargo = data.IdCargo;
            candidato.IdTranspariencia = data.IdTranspariencia;
            candidato.DateRegister = DateTime.Now;
            candidato.UserRegister = _username;
            candidato.IpRegister = _ip;

            await _context.Candidatos.AddAsync(candidato);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado el candidato");
            return infoDTO;

        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var candidatoToDelete = await _context.Candidatos.Where(x => x.Active && x.IdCandidato == id).FirstOrDefaultAsync();
            if (candidatoToDelete != null)
            {
                infoDTO.AccionFallida("El candidato seleccionado no se encuentra disponible", 400);
            }

            candidatoToDelete.DateDelete = DateTime.Now;
            candidatoToDelete.Active = false;
            candidatoToDelete.UserDelete = _username;
            candidatoToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("El candidato seleccionado a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(CandidatoDTO data)
        {
            try
            {
                var model = await _context.Candidatos.Where(x => x.Active && x.IdCandidato == data.IdCandidato).FirstOrDefaultAsync() ?? throw new Exception("No se encontro el candidato");

                model.NombreCandidato = data.NombreCandidato;
                model.Edad = data.Edad;
                model.FotoUrl = data.FotoUrl;
                model.LugarDeNacimiento = data.LugarDeNacimiento;
                model.InformacionDeContacto = data.InformacionDeContacto;
                model.IdPartido = data.IdPartido;
                model.IdCargo = data.IdCargo;
                model.IdTranspariencia = data.IdTranspariencia;

                await _context.SaveChangesAsync();

                return infoDTO;
            }catch(Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "CandidatoRepository", "Error al intentar actualizar el candidato");
            }
        }

        public async Task<CandidatoDTO> Get(long id)
        {
            var candidatoSelected = await _context.Candidatos.Where(x => x.Active && x.IdCandidato == id).Select(c => new CandidatoDTO
            {
                IdCandidato = c.IdCandidato,
                NombreCandidato = c.NombreCandidato,
                Edad = c.Edad,
                FotoUrl = c.FotoUrl,
                LugarDeNacimiento = c.LugarDeNacimiento,
                InformacionDeContacto = c.InformacionDeContacto,
                IdPartido = c.IdPartido,
                IdCargo = c.IdCargo,
                IdTranspariencia = c.IdTranspariencia,
            }).FirstOrDefaultAsync();

            return candidatoSelected;
        }

        public async Task<List<MostrarCandidatoDTO>> GetAll()
        {
            var candidatos = await _context.Candidatos.Where(x => x.Active).Select(c => new MostrarCandidatoDTO
            {
                IdCandidato = c.IdCandidato,
                NombreCandidato = c.NombreCandidato,
                Edad = c.Edad,
                FotoUrl = c.FotoUrl,
                LugarDeNacimiento = c.LugarDeNacimiento,
                InformacionDeContacto = c.InformacionDeContacto,
                NombrePartido = c.Partido.NombrePartido,
                Cargo = c.Cargo.Nombre,
                DeclaracionDeBienes = c.Transpariencia.DeclaracionesDeBienes,
                InvolucradoEnEscandalos = c.Transpariencia.InvolucradoEnEscandalos,
                EvaluacionesDeEtica = c.Transpariencia.EvaluacionesDeEtica,
            }).ToListAsync();

            return candidatos;
        }

        public async Task<List<KeyValueDTO>> KeyValue()
        {
            var cantidatoSelector = await _context.Candidatos.Where(x => x.Active).Select(c => new KeyValueDTO
            {
                Key = c.IdCandidato,
                Value = c.NombreCandidato,
            }).ToListAsync();

            return cantidatoSelector;
        }
    }
}
