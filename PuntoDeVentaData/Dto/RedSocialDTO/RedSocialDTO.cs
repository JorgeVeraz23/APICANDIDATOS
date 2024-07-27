﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto.RedSocialDTO
{
    public class RedSocialDTO
    {
        public long IdResocial { get; set; }

        public string Plataforma { get; set; } = "";
        [Required]
        public string Url { get; set; }
        public long? IdCandidato { get; set; }
    }

    public class MostrarRedSocialDTO
    {
        public long IdResocial { get; set; }

        public string Plataforma { get; set; } = "";
        [Required]
        public string Url { get; set; }
        public string NombreCandidato { get; set; } = "";
    }
}
