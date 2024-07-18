using Data.Entities.Candidatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.TransparienciaDTO
{
    public class TransparienciaDTO
    {
        public long IdTranspariencia { get; set; }
        public string DeclaracionesDeBienes { get; set; } = "";
        public bool InvolucradoEnEscandalos { get; set; }
        public string EvaluacionesDeEtica { get; set; } = "";
    }


    public class MostrarTransparienciaDTO
    {
        public long IdTranspariencia { get; set; }
        public string DeclaracionesDeBienes { get; set; } = "";
        public bool InvolucradoEnEscandalos { get; set; }
        public string EvaluacionesDeEtica { get; set; } = "";
        public string NombreCandidato { get; set; } = "";

    }
}
