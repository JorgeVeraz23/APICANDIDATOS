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
    public class MenuRole : CrudEntities
    {
        [Key]
        public long IdMenuRole { get; set; }
        public bool IsVisible { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsSendEmail { get; set; }
        public bool IsPrint { get; set; }
        public bool IsDownloadExcel { get; set; }
        public bool IsDownloadPDF { get; set; }
        public bool IsProcess { get; set; }
        public bool IsApproved { get; set; }
        [ForeignKey("Rol")]
        public string? IdRole { get; set; }
        public ApplicationRole? Role { get; set; }
        [ForeignKey("Menu")]
        public long? IdMenu { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
