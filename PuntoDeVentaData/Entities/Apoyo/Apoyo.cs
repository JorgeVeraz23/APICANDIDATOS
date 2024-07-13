using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Apoyo
{
    public class Apoyo : CrudEntities
    {
        [Key]
        public long IdApoyo { get; set; }
        [Required]
        [MaxLength(250)]
        public string NombreDelPartidario { get; set; } = "";
        public string Descripcion { get; set; } = "";
        [ForeignKey("Candidato")]
        public long IdCandidato { get; set; }
        public virtual Candidato Candidato { get; set; }
    }
}
