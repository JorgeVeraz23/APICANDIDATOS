using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.PartidosPoliticos
{
    public class Partido : CrudEntities
    {
        [Key]
        public long IdPartido { get; set; }
        [Required]
        [MaxLength(100)]
        public string NombrePartido { get; set; } = "";
        public virtual ICollection<Candidato>? Candidatos { get; set; }
    }
}
