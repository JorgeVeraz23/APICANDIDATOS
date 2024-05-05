using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Parameters
{
    public class ParameterType : CrudEntities
    {
        [Key]
        public long IdTypeParameter { get; set; }
        [Required(ErrorMessage = "El {0} es Requerido")]
        [StringLength(maximumLength: 200, ErrorMessage = "La longitud maxima es de 200 caracteres")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El {0} es Requerido")]
        [StringLength(maximumLength: 200, ErrorMessage = "La longitud máxima es de 200 caracteres")]
        public string Icon { get; set; } = "Home";
        public int Orden { get; set; }
        //Virtual
        public virtual ICollection<Parameters> Parameters { get; set; }
    }
}
