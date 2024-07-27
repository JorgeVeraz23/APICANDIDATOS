using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.ExperienciaDTO
{
    public class TrayectoriaDTO
    {
        public long IdTrayectoria { get; set; }

        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public long? IdCandidato { get; set; }
    }

    public class MostrarTrayectoriaDTO
    {
        public long IdTrayectoria { get; set; }

        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string NombreCandidato { get; set; } = "";
    }
}
