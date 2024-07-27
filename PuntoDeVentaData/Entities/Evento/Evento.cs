using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Evento
{
    public class Evento : CrudEntities
    {
        [Key]
        public long IdEvento { get; set; }
        [Required]
        [MaxLength(250)]
        public string Titulo { get; set; } = "";
        [Required]
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; } = "";
        [ForeignKey("Candidato")]
        public long? IdCandidato { get; set; }
        public virtual Candidato? Candidato { get; set; }


    }
}
