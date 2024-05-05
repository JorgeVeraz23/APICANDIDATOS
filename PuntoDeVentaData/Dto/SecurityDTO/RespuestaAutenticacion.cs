using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.SecurityDTO
{
    public class RespuestaAutenticacion
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; } 
        public string Ambiente { get; set; }
        public string BaseDatos { get; set;  } = string.Empty;
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
