using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.PocisionDTO
{
    public class PocisionDTO
    {
        public long IdPocision { get; set; }

        public string Tema { get; set; } = "";

        public string Postura { get; set; } = "";
        public long? IdCandidato { get; set; }
    }

    public class MostrarPocisionDTO
    {
        public long IdPocision { get; set; }

        public string Tema { get; set; } = "";

        public string Postura { get; set; } = "";
        public string NombrePocision = "";
    }

}
