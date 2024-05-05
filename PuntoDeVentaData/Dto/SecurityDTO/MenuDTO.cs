using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.SecurityDTO
{
    public class MenuDTO
    {
        public long IdMenu { get; set; }
        public long? IdMenuPadre { get; set; }
        public int Nivel { get; set; }
        public int Orden { get; set; }
        [Unicode]
        [Required(ErrorMessage = "El {0} es Requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "La longitud máxima es de 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El {0} es Requerido")]
        [StringLength(maximumLength: 500, ErrorMessage = "La longitud máxima es de 500 caracteres")]
        public string Descripcion { get; set; } = string.Empty;
        [StringLength(maximumLength: 100, ErrorMessage = "La longitud máxima es de 100 caracteres")]
        public string? Controlador { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "La longitud máxima es de 100 caracteres")]
        public string? Vista { get; set; }
        [StringLength(maximumLength: 1000, ErrorMessage = "La longitud máxima es de 1000 caracteres")]
        public string? UrlAbsoluta { get; set; }
        [StringLength(maximumLength:50, ErrorMessage ="La longitud maxima es de 50 caracteres")]
        public string? Icon { get; set; }
        [StringLength(maximumLength:50, ErrorMessage ="La longitud maxima es de 50 caracteres" )]
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
    }

    public class ItemMenuDTO : MenuDTO
    {
        [Unicode]
        public string Codigo { get; set; } = "01";
    }
}
