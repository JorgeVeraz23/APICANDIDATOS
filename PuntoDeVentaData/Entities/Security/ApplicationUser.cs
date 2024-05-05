using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Security
{
    public class ApplicationUser : IdentityUser 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public bool Bloqueo { get; set; } = false;
        public string? Identificacion { get; set; }
        public string? Description { get; set; }
        public string? RutaFoto { get; set; }
        public bool? Activo { get; set; } = true;
        [JsonIgnore]
        [MaxLength(100)]
        public string? UsuarioRegistro { get; set; }
        [JsonIgnore]
        public DateTime FechaRegistro { get; set; }
        [JsonIgnore]
        [MaxLength(100)]
        public string? IpRegistro { get; set; }
        [JsonIgnore]
        [MaxLength(100)]
        public string? UsuarioModificacion { get; set; }
        [JsonIgnore]
        [MaxLength(100)]
        public DateTime? FechaModificacion { get; set; }
        [JsonIgnore]
        [MaxLength(100)]
        public string? IpModificacion { get; set; }
        [JsonIgnore]
        [MaxLength(100)]
        public string? UsuarioEliminacion { get; set; }
        [JsonIgnore]
        public DateTime? FechaEliminacion { get; set; }
        [JsonIgnore]
        [MaxLength(100)]
        public string? IpEliminacion { get; set; }

    }
}
