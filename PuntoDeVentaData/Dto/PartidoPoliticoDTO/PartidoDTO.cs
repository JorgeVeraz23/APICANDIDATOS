using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.PartidoPoliticoDTO
{
    public class PartidoDTO
    {
        public long IdPartido { get; set; }
        public string NombrePartido { get; set; } = "";
    }
}
