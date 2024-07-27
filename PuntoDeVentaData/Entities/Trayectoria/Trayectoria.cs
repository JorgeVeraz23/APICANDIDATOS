using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Trayectoria
{
    public class Trayectoria : CrudEntities
    {
        [Key]
        public long IdTrayectoria { get; set; }
        [Required]
        [MaxLength(250)]
        public string Titulo { get; set; } = "";
        [MaxLength(250)]
        public string Descripcion { get; set; } = "";
        [ForeignKey("Candidato")]
        public long? IdCandidato { get; set; }
        public virtual Candidato? Candidato { get; set; }

    }
}
