using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Security
{
    public class ApplicationRole : IdentityRole
    {
        public bool Active { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserRegister { get; set; } = "SYSTEM";
        [Required]
        public DateTime DateRegister { get; set; }
        [MaxLength(100)]
        public string? IpRegister { get; set; }
        [MaxLength(100)]
        public string? UserModification { get; set; }
        [MaxLength(100)]
        public DateTime? DateModification { get; set; }
        [MaxLength(100)]
        public string? IpModification { get; set;}
        [MaxLength(100)]
        public string? UserDelete { get; set; }
        public DateTime DateDelete { get; set; }
        [MaxLength(100)]
        public string? IpDelete { get; set;}
        public ICollection<MenuRole> MenuRoles { get; set; }

    }
}
