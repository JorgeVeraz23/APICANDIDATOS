using DocumentFormat.OpenXml.Wordprocessing;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Templates
{
    public class EmailTemplate : CrudEntities
    {
        [Key]
        public long IdEmailTemplate {  get; set; }
        [MaxLength(20), MinLength(20)]
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        public string Enumerator { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [StringLength(maximumLength: 200, ErrorMessage = "La longitud máxima es de 200 caracteres")]
        public string Subject { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [Column("Body", TypeName = "ntext")]
        public string Body { get; set; } = "<body></body>";
        public string Params { get; set; } = string.Empty;
    }
}
