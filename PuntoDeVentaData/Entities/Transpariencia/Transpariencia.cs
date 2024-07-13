using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Transpariencia
{
    public class Transpariencia : CrudEntities
    {
        [Key]
        public long IdTranspariencia { get; set; }
        public string DeclaracionesDeBienes { get; set; } = "";
        public bool InvolucradoEnEscandalos { get; set; }
        public string EvaluacionesDeEtica { get; set; } = "";
        public virtual Candidato? Candidato { get; set; }
    }
}
