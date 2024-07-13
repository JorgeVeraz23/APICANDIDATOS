using Data.Entities.Candidatos;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Cargo
{
    public class Cargo : CrudEntities
    {
        [Key]
        public long IdCargo { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = "";
        public ICollection<Candidato>? Candidato { get; set; }
    }
}
