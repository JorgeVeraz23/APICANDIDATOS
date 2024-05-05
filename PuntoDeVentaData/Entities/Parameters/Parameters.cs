using Microsoft.EntityFrameworkCore;
using PuntoDeVentaData.Entities.Enumerators;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Parameters
{
    public class Parameters : CrudEntities
    {
        [Key]
        public long IdParameter { get; set; }
        [Unicode]
        [Required(ErrorMessage ="El {0} es Requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "La longitud maxima es de 50 caracteres")]
        public string EnumParameter { get; set; } = string.Empty; // Hace referencia al Enumerador EnumParams para validaciones
        [Required(ErrorMessage = "El {0} es Requerido")]
        [StringLength(maximumLength: 200, ErrorMessage = "La longitud máxima es de 200 caracteres")]
        public string Value { get; set; } = "NA";
        public string EmailList = "jorgeveraug2@gmail.com";
        //public string EmailList = { }
        [Required(ErrorMessage = "El {0} es Requerido")]
        public EnumTypeDate DataType { get; set; }
        [StringLength(maximumLength:500, ErrorMessage ="La longitud máxima es de 500 caracteres")]
        public string? Comentary { get; set; }
        public int Orden { get; set; }
        [ForeignKey("TipoParametro")]
        public long IdTypeParameter { get; set; }
        public virtual ParameterType? TypeParameter { get; set; }
    
    }
}
