﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Propuestas;

namespace Data.Dto.CandidatoDTO
{
    public class CandidatoDTO
    {
        public long IdCandidato { get; set; }
        public string NombreCandidato { get; set; } = "";
        public int Edad { get; set; }
        public string FotoUrl { get; set; } = "";
        public string LugarDeNacimiento { get; set; } = "";
        public string InformacionDeContacto { get; set; } = "";
        public long? IdPartido { get; set; }
        public long? IdCargo { get; set; }
        public long? IdTranspariencia { get; set; }

    }

    public class MostrarCandidatoDTO
    {
        public long IdCandidato { get; set; }
        public string NombreCandidato { get; set; } = "";
        public int Edad { get; set; }
        public string FotoUrl { get; set; } = "";
        public string LugarDeNacimiento { get; set; } = "";
        public string InformacionDeContacto { get; set; } = "";
        public string NombrePartido { get; set; } = "";
        public string Cargo { get; set; } = "";
        public string DeclaracionDeBienes { get; set; } = "";
        public bool InvolucradoEnEscandalos { get; set; }
        public string EvaluacionesDeEtica { get; set; } = "";

    }

    public class MostrarCandidatoConDetalleDTO
    {
        public long IdCandidato { get; set; }
        public string NombreCandidato { get; set; } = "";
        public int Edad { get; set; }
        public string FotoUrl { get; set; } = "";
        public string LugarDeNacimiento { get; set; } = "";
        public string InformacionDeContacto { get; set; } = "";
        public string NombrePartido { get; set; } = "";
        public string Cargo { get; set; } = "";
        public List<PropuestaDTO.PropuestaDTO> Propuestas { get; set; } = new List<PropuestaDTO.PropuestaDTO>();


    }

    public class CrearCandidatoDTO
    {
        public string NombreCandidato { get; set; }
        public int Edad { get; set; }
        public string FotoUrl { get; set; }
        public string LugarDeNacimiento { get; set; }
        public string InformacionDeContacto { get; set; }
        public long IdPartido { get; set; }
        public long IdCargo { get; set; }
        public string NombreTransparencia { get; set; }
        public string DescripcionTransparencia { get; set; }
        public bool InvolucradoEnEscandalos { get; set; }
    }
}
