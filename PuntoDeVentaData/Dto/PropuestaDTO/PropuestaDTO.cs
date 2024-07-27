using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.PropuestaDTO
{
    public class PropuestaDTO
    {
        public long IdPropuesta { get; set; }
        [Required]
        [MaxLength(500)]
        public string Titulo { get; set; } = "";
        [Required]
        public string Descripción { get; set; } = "";
        public string Area { get; set; } = "";
        public long? IdCandidato { get; set; }
    }

    public class MostrarPropuestaDTO
    {
        public long IdPropuesta { get; set; }
        public string Titulo { get; set; } = "";
        [Required]
        public string Descripción { get; set; } = "";
        public string Area { get; set; } = "";
        public string NombreCandidato { get; set; } = "";
    }
}
