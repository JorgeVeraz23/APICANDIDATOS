using Data.Entities.Candidatos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Pocision
{
    public class Pocision : CrudEntities
    {
        [Key]
        public long IdPocision { get; set; }
        [Required]
        [MaxLength(250)]
        public string Tema { get; set; } = "";
        [Required]
        [MaxLength(500)]
        public string Postura { get; set; } = "";
        [ForeignKey("Candidato")]
        public long IdCandidato { get; set; }
        public virtual Candidato? Candidato { get; set; }
    }
}
