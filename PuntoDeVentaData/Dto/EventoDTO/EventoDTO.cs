using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Data.Dto.EventoDTO
{
    public class EventoDTO
    {
        public long IdEvento { get; set; }
        public string Titulo { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; } = "";
        public long IdCandidato { get; set; }
    }

    public class MostrarEventoDto
    {
        public long IdEvento { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; }
        public string NombreEvento { get; set; }
    }
}
