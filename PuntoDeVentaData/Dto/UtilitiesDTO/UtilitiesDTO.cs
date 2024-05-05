using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.UtilitiesDTO
{
    public class DropDownWebDTO
    {
        public string Text { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class DropDownDTO
    {
        public string Key { get; set; }
    }
}
