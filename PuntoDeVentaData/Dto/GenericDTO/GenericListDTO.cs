using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Dto.GenericDTO
{
    public class GenericListDTO
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("Active")]
        public bool Active { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }

    }
}
