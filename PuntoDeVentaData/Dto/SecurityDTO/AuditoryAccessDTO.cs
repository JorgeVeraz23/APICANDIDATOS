using Microsoft.EntityFrameworkCore;
using PuntoDeVentaData.Entities.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.SecurityDTO
{
    public class AuditoryAccessDTO
    {
        public long IdAuditoryAccesDto { get; set; }
        [Unicode]
        [Required(ErrorMessage = "El {0} es Requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "La longitud máxima es de 50 caracteres")]
        public string User { get; set; } = "SYSTEM";
        public EnumAccessType Type { get; set; }
        public DateTime DateAdminission { get; set; }


    }

    public class AuditoriaAccesosEliminacionDTO
    {
        public long IdAuditoryAccess { get; set;}

        public string UserDelete { get; set; } = string.Empty;
        public string IpDelete { get; set; } = string.Empty;
    }
}
