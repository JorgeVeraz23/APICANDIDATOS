using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.RedSocial
{
    public class RedSocial : CrudEntities
    {
        [Key]
        public long IdRedsocial { get; set; }
        [Required]
        [MaxLength(250)]
        public string Plataforma { get; set; } = "";
        [Required]
        public string Url { get; set; }
        [ForeignKey("Candidato")]
        public long? IdCandidato { get; set; }
        public virtual Candidato? Candidato { get; set; }
    }
}
