using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Utilities
{
    public class CrudEntities
    {
        public bool Active { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserRegister { get; set; } = "SYSTEM";
        [Required]
        public DateTime DateRegister { get; set; }
        [JsonIgnore]
        [MaxLength (100)]
        public string? IpRegister { get; set; }
        [JsonIgnore]
        [MaxLength (100)]
        public string? UserModification { get; set; }
        [JsonIgnore]
        [MaxLength (100)]
        public DateTime? DateModification { get; set; }
        [JsonIgnore]
        [MaxLength (100)]
        public string? IpModification { get; set;}
        [JsonIgnore]
        [MaxLength (100)]
        public string? UserDelete { get; set; }
        [JsonIgnore]
        public DateTime? DateDelete { get; set;}
        [JsonIgnore]
        [MaxLength (100)]
        public string? IpDelete { get; set; }
    }
}
