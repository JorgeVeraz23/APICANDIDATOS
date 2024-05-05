using Data.Dto.TemplateDTO;
using Data.Interfaces.SecurityInterfaces;
using Data.Interfaces.TemplateInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.TemplateRepository
{
    public class TemplateRepository : IEmailTemplate
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private IUnitOfWorkRepository _unit;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _nombreRepositorio;

        public TemplateRepository(ApplicationDbContext context, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;
            _nombreRepositorio = "PlantillaCorreoRepository";
        }

        public async Task<ItemEmailTemplateDTO> BuscarPlantillaCorreo(string enumerador)
        {
            try
            {
                var plantillaCorreo = await _context.EmailTemplates
                    .Where(p => p.Active && p.Enumerator.Contains(enumerador))
                    .Select(plantilla => new ItemEmailTemplateDTO
                    {
                        IdEmailTemplate = plantilla.IdEmailTemplate,
                        Enumerator = plantilla.Enumerator,
                        Subject = plantilla.Subject,
                        Body = plantilla.Body,

                    }).FirstOrDefaultAsync();
                return plantillaCorreo;
            }
            catch
            {
                throw;
            }


        }

        public async Task<MessageInfoDTO> InsertarPlantillaCorreo(EmailTemplate plantillaCorreo)
        {
            try
            {
                plantillaCorreo.Active = true;
                plantillaCorreo.DateRegister = DateTime.Now;
                plantillaCorreo.Params = "1";
                var result = await _context.EmailTemplates.AddAsync(plantillaCorreo);
                await _unit.SaveChangesAsync();
                return infoDTO.AccionCompletada("Registro de plantilla exitosa.");

            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nombreRepositorio, "Error al registrar platilla de correo");
            }
        }

        public async Task<List<ItemEmailTemplateDTO>> ListarPlantillaCorreo()
        {
            List<ItemEmailTemplateDTO> listaPantillaCorreo = new();
            try
            {
                listaPantillaCorreo = await _context.EmailTemplates.Where(p => p.Active).Select(plantilla => new ItemEmailTemplateDTO
                {
                    IdEmailTemplate = plantilla.IdEmailTemplate,
                    Enumerator = plantilla.Enumerator,
                    Subject = plantilla.Subject,
                    Body = plantilla.Body,

                }).ToListAsync();

            }
            catch
            {
                throw;
            }

            return listaPantillaCorreo;

        }

        public async Task<MessageInfoDTO> ModificarPlantillaCorreo(EmailTemplate plantillaCorreo)
        {
            try
            {
                var _plantillaCorreo = await _context.EmailTemplates
                    .Where(p => p.Active && p.IdEmailTemplate == plantillaCorreo.IdEmailTemplate)
                    .FirstOrDefaultAsync();
                if (_plantillaCorreo is null)
                {
                    return infoDTO.AccionFallida("No se encontro la plantilla.", 404);
                }
                _plantillaCorreo.Subject = plantillaCorreo.Subject;
                _plantillaCorreo.Body = plantillaCorreo.Body;
                _plantillaCorreo.Enumerator = plantillaCorreo.Enumerator;

                _plantillaCorreo.DateModification = DateTime.Now;
                _plantillaCorreo.IpModification = plantillaCorreo.IpModification;
                _plantillaCorreo.UserModification = plantillaCorreo.UserModification;
                await _unit.SaveChangesAsync();
                return infoDTO.AccionCompletada("Se ha actualizado la plantilla exitosamente");

            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nombreRepositorio, "Error al editar la plantilla");
            }
        }

        public async Task<MessageInfoDTO> EliminarPlantillaCorreo(string enumerador, string ip, string usuario)
        {
            try
            {
                var plantillaCorreo = await _context.EmailTemplates
                    .Where(p => p.Active && p.Enumerator.Contains(enumerador))
                    .FirstOrDefaultAsync();
                if (plantillaCorreo == null)
                {
                    return infoDTO.AccionFallida("No se encontro la plantilla con el numerador indicado", 404);
                }
                plantillaCorreo.Active = false;
                plantillaCorreo.IpDelete = ip;
                plantillaCorreo.UserDelete = usuario;
                plantillaCorreo.DateDelete = DateTime.Now;
                await _unit.SaveChangesAsync();
                return infoDTO.AccionCompletada("La plantilla se elimino correctamente");
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, _nombreRepositorio, "Error al eliminar plantilla correo:");
            }
        }
    }
}
