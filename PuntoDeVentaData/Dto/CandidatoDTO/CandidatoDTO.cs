using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.CandidatoDTO
{
    public class CandidatoDTO
    {
        public long IdCandidato { get; set; }
        public string NombreCandidato { get; set; } = "";
        public int Edad { get; set; }
        public string FotoUrl { get; set; } = "";
        public string LugarDeNacimiento { get; set; } = "";
        public string InformacionDeContacto { get; set; } = "";
        public long IdPartido { get; set; }
        public long IdCargo { get; set; }
        public long IdTranspariencia { get; set; }

    }

    public class MostrarCandidatoDTO
    {
        public long IdCandidato { get; set; }
        public string NombreCandidato { get; set; } = "";
        public int Edad { get; set; }
        public string FotoUrl { get; set; } = "";
        public string LugarDeNacimiento { get; set; } = "";
        public string InformacionDeContacto { get; set; } = "";
        public string NombrePartido { get; set; } = "";
        public string Cargo { get; set; } = "";
        public string NombreTransparencia { get; set; } = "";


    }
}
