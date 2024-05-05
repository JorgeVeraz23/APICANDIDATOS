using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.SecurityDTO
{
    public class RolDTO
    {
        public string Id { get; set; } = string.Empty;
        public string RolName { get; set; } = string.Empty;
        [JsonIgnore]
        public string nameUsuario { get; set; } = "";
        [JsonIgnore]
        public string ipUsuario { get; set; }
    }

    public class ItemRolDTO
    {
        public string Id { get; set; } = string.Empty;
        public string RolName { get; set; } = string.Empty;
    }
}
