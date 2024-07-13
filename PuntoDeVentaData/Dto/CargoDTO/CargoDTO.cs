using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.CargoDTO
{
    public class CargoDTO
    {
        public long IdCargo { get; set; }
        public string Nombre { get; set; } = "";
    }
}
