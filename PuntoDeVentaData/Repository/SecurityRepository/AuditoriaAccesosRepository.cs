using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.ComponentModel.DataAnnotations;
using Data.Interfaces.SecurityInterfaces;
using Data;
using PuntoDeVentaData.Entities.Security;
using PuntoDeVentaData.Dto.SecurityDTO;
namespace LinkQuality.Data.Repository.SeguridadRepository
{
    public class AuditoriaAccesosRepository : IAuditoriaAccesosRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private IUnitOfWorkRepository _unit;

        public AuditoriaAccesosRepository(ApplicationDbContext context, IServiceProvider serviceProvider,
            IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;
        }

        public async Task<List<AuditoryAccess>> GetAuditoriaAccesos() => await _context.AuditoryAccesses.ToListAsync();

        public async Task<bool> RegistrarAuditoriaAccesos(AuditoryAccess auditoriaAccesos)
        {
            var obj = await _context.AuditoryAccesses.FirstOrDefaultAsync(a => a.IdAuditoryAccess == auditoriaAccesos.IdAuditoryAccess);
            bool esNuevo = false;
            if (obj == null)
            {
                obj = new AuditoryAccess();
                esNuevo = true;
            }

            obj.User = auditoriaAccesos.User;
            obj.Type = auditoriaAccesos.Type;
            obj.DateAdmission = auditoriaAccesos.DateAdmission;
            obj.Active = true;

            if (esNuevo)
            {
                obj.DateRegister = DateTime.Now;
                obj.UserRegister = auditoriaAccesos.UserRegister;
                obj.IpRegister = auditoriaAccesos.IpRegister;
                await _context.AddAsync(obj);
            }
            else
            {
                obj.DateModification = DateTime.Now;
                obj.UserModification = auditoriaAccesos.UserModification;
                obj.IpModification = auditoriaAccesos.IpModification;
            }


            /* await Task.Delay(5000);*/

            try
            {

                await _unit.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException e)
            {
                throw new ValidationException(e.Message, e?.InnerException);
            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message, ex?.InnerException);
            }
        }

        public async Task<bool> EliminarAuditoriaAccesos(AuditoriaAccesosEliminacionDTO auditoriaAccesosEliminacion)
        {
            var obj = await _context.AuditoryAccesses.FindAsync(auditoriaAccesosEliminacion.IdAuditoryAccess);

            if (obj == null)
            {
                return false;
            }
            obj.Active = false;
            obj.DateDelete = DateTime.Now;
            obj.UserDelete = auditoriaAccesosEliminacion.UserDelete;
            obj.IpDelete = auditoriaAccesosEliminacion.IpDelete;
            await _unit.SaveChangesAsync();
            return true;
        }


    }
}
