using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Utilities
{
    public class Notificacion : CrudEntities
    {
        [Key]
        public long IdNotification { get; set; }
        [MaxLength(50), MinLength(5)]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string Title { get; set; } = string.Empty;
        [MinLength(5)]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string Texto { get; set; } = "No disponible";
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string JsonUsersDestinatarios { get; set; } = "[]";
        [ForeignKey("UserOrigen")]
        public string IdUserOrigen { get; set; } = string.Empty;
        public virtual ApplicationUser userOrigen { get; set; } = new();
    }
}
