using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.UtilitiesDTO
{
    public class NotificationDTO
    {
        public long IdNotificacion { get; set; }
        [MaxLength(50), MinLength(5)]
        [Required(ErrorMessage ="El campo {0} es Requerido")]
        public string Title { get;set; }
        [MinLength(5)]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string Texto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCaducidad { get; set; }
        [Required(ErrorMessage = "El campo destinatarios es Requerido")]
        public string JsonUsersDestinatarios { get; set; } = "{}";
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string IdUserOrigen { get; set; } = string.Empty;
        public List<NotificacionDestinatarioDTO> Destinatarios { get; set; } = [];
    }

    public class NotificacionDestinatarioDTO
    {
        public string IdDestinatario { get; set; } = string.Empty;
        public string NombreDestinatario { get; set; } = string.Empty;
        public string RutaFoto { get; set; } = string.Empty;

    }

    public class ObjetoEliminacionDTO
    {
        public long Id { get; set; }
        public string UsuarioEliminacion { get; set; } = string.Empty;
        public string IpEliminacion { get; set; } = string.Empty;
    }

    public class ObjetoEliminacionStringDTO
    {
        public string Id { get; set; } = string.Empty;
        public string UsuarioEliminacion { get; set; } = string.Empty;
        public string IpEliminacion { get; set; } = string.Empty;
    }
}
