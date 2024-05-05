using PuntoDeVentaData.Entities.Enumerators;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Security
{
    public class AuditoryAccess : CrudEntities
    {
        [Key]
        public long IdAuditoryAccess { get; set; }
        [Required(ErrorMessage = " El {0} es obligatorio")]
        [StringLength(maximumLength: 50, ErrorMessage ="La longitud máxima es de 50 caracteres")]
        public string User { get; set; }
        public EnumAccessType Type { get; set; }
        public DateTime DateAdmission { get; set; }

    }
}
