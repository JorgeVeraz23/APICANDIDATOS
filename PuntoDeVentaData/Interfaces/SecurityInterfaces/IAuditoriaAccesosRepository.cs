using PuntoDeVentaData.Dto.SecurityDTO;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.SecurityInterfaces
{
    public interface IAuditoriaAccesosRepository
    {
        public Task<List<AuditoryAccess>> GetAuditoriaAccesos();
        public Task<bool> RegistrarAuditoriaAccesos(AuditoryAccess auditoryAccess);
        public Task<bool> EliminarAuditoriaAccesos(AuditoriaAccesosEliminacionDTO auditoriaAccesosEliminacionDTO);
    }
}
