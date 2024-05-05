using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Security
{
    public class Menu : CrudEntities
    {
        [Key]
        public long IdMenu { get; set; }
        [ForeignKey("MenuFather")]
        public long? IdMenuFather { get; set; }
        [Required]
        public int Nivel { get; set; } = 1;
        [Required]
        public int Orden { get; set; } = 1;
        [Required]
        public string Code { get; set; } = "01";
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(200)]
        public string? Description { get; set; }
        [StringLength(200)]
        public string? Controller { get; set; }
        [StringLength(200)]
        public string? View { get; set; }
        [StringLength(500)]
        public string? AbsoluteURL { get; set; }
        [StringLength(500)]
        public string? RelativeURL { get; set; }
        [StringLength(100)]
        public string? Icon { get; set; }
        [StringLength(50)]
        public string? ColorRef { get; set; }
        public bool IsVisible { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsSendEmail { get; set; }
        public bool IsPrint { get; set; }
        public bool IsDownloadExcel { get; set; }
        public bool IsDownloadPDF { get; set; } 
        public bool IsProcess { get; set; }
        public bool IsApproved { get; set; }
        public virtual Menu? MenuFather { get; set; }
        public virtual ICollection<MenuRole> MenuRoles { get; set; }

    }
}
