using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Propuestas
{
    public class Propuesta : CrudEntities
    {
        [Key]
        public long IdPropuesta { get; set; }
        [Required]
        [MaxLength(500)]
        public string Titulo { get; set; } = "";
        [Required]
        public string Descripción { get; set; } = "";
        public string Area { get; set; } = "";
        [ForeignKey("Candidato")]
        public long IdCandidato { get; set; }
        public virtual Candidato? Candidato { get; set; }



    }
}
