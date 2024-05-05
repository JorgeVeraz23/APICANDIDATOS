using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Utilities
{
    public class Logs
    {
        [Key]
        public long IdLog { get; set; }
        [MaxLength(100)]
        public string RequestId { get; set; } = string.Empty;
        [MaxLength(100)]
        public string RequestTraceIdentifier { get; set; }  = string.Empty;
        public DateTime Fecha { get; set; }
        [MaxLength(100)]
        public string Controller { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Endpoint { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string StackStrace { get; set; } = string.Empty;
        public string InnerException { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Plataform { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Ambiente { get; set; } = string.Empty;
        
    }
}
