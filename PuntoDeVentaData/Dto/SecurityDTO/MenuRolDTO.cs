using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.SecurityDTO
{
    public class MenuRolDTO
    {
        public long IdMenuRol { get; set; }
        public bool IsVisible { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsSendEmail { get; set; }
        public bool IsPrint { get; set; }
        public bool IsDownloadExcel { get; set; }
        public bool IsDownloadPDF { get; set; }
        public bool IsProcess { get; set; }
        public bool IsApproved { get; set; }
        //FK
        [Required] public string IdRol { get; set; } = string.Empty;
        [Required] public long IdMenu { get; set; }
    }

    public class MenuRolConsultaDto : ItemMenuDTO
    {
        public long IdMenuRol { get; set; }
        public string IdRol { get; set; }
        public IdentityRole IdentityRole { get; set; } = new();
    }

    public class MainMenuRoleDTO
    {
        public long IdMenuRole { get; set; } = -1;
        public IdentityRole IdentityRole { get; set; } = new();
        public string? Controller { get; set; }
        public string? View { get; set; }
        public string? Icon { get; set; }
        public string Name { get; set; }
        public List<SubMenuRoleDTO> SubItems { get; set; } = new();
        public PermissionsMenuRoleDTO? PermissionsMenuRole { get; set; }

    }

    public class SubMenuRoleDTO : MainMenuRoleDTO { }

    public class PermissionsMenuRoleDTO { 
        public bool IsVisible { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsSendEmail { get; set; }
        public bool IsPrint { get; set; }
        public bool IsDownloadExcel { get; set; }
        public bool IsDownloadPDF {  get; set; }
        public bool IsProcess { get; set; }
        
    }

    public class MenuRolEliminacionDTO
    {
        public long IdMenuRole { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string IpEliminacion { get; set;}
    }

}
