using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.ApoyoDTO
{
    public class ApoyoDTO
    {
        public long IdApoyo { get; set; }
        public string NombreDelPartidario { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public long IdCandidato { get; set; }
    }

    public class MostrarApoyoDTO
    {
        public long IdApoyo { get; set; }
        public string NombreDelPartidario { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string NombreCandidato { get; set; } = "";
    }
}
