using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Configurations
{
    public class ApplicationVersion : CrudEntities
    {
        [Key]
        public long IdApplicationVersion { get; set; }
        [Required]
        [StringLength(100)]
        public string VersionName { get;set; }
        [Required]
        [StringLength(100)]
        public string PlatForm {  get; set; }
    }
}
