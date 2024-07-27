using Data.Dto.CargoDTO;
using Data.Dto.UtilitiesDTO;
using Data.Entities.Candidatos;
using Data.Entities.Cargo;
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
    public class CargoRepository : CargoInterface
    {
        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public CargoRepository(
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

        public async Task<MessageInfoDTO> Create(CargoDTO data)
        {
            var isAlreadyExist = await _context.Cargos.Where(x => x.Active && x.Nombre.ToUpper().Equals(data.Nombre.ToUpper())).AnyAsync();

            if (isAlreadyExist)
            {
                infoDTO.AccionFallida("Ya existe un Motivo registrado con ese nombre", 400);
                return infoDTO;
            }

            Cargo cargo = new Cargo();
            cargo.Active = true;
            cargo.Nombre = data.Nombre;
            cargo.DateRegister = DateTime.Now;
            cargo.UserRegister = _username;
            cargo.IpRegister = _ip;

            await _context.Cargos.AddAsync(cargo);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado el cargo");
            return infoDTO;
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var cargoToDelete = await _context.Cargos.Where(x => x.Active && x.IdCargo == id).FirstOrDefaultAsync();
            if (cargoToDelete != null)
            {
                infoDTO.AccionFallida("El cargo seleccionado no se encuentra disponible", 400);
            }
            cargoToDelete.DateDelete = DateTime.Now;
            cargoToDelete.Active = false;
            cargoToDelete.UserDelete = _username;
            cargoToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("El cargo seleccionado a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(CargoDTO data)
        {
            try
            {
                var model = await _context.Cargos.Where(x => x.Active && x.IdCargo == data.IdCargo).FirstOrDefaultAsync() ?? throw new Exception("No se encontro el cargo");

                model.Nombre = data.Nombre;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "CargoRepository", "Error al intentar actualizar el cargo");
            }
        }

        public async Task<CargoDTO> Get(long id)
        {
            var cargoSelected = await _context.Cargos.Where(x => x.Active && x.IdCargo == id).Select(c => new CargoDTO
            {
                IdCargo = c.IdCargo,
                Nombre = c.Nombre,
            }
              ).FirstOrDefaultAsync();
            return cargoSelected;
        }

        public async Task<List<CargoDTO>> GetAll()
        {
            var cargos = await _context.Cargos.Where(x => x.Active).Select(c => new CargoDTO
            {
                IdCargo = c.IdCargo,
                Nombre = c.Nombre,

            }).ToListAsync();
            return cargos;
        }

        public async Task<List<KeyValueDTO>> KeyValueCargo()
        {
            var selectorCargo = await _context.Cargos.Where(x => x.Active).Select(c => new KeyValueDTO
            {
                Key = c.IdCargo,
                Value = c.Nombre,
            }).ToListAsync();


            return selectorCargo;
        }
    }
}
