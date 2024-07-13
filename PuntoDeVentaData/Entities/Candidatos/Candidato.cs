using Data.Entities.Apoyo;
using Data.Entities.Evento;
using Data.Entities.Experiencia;
using Data.Entities.PartidosPoliticos;
using Data.Entities.Pocision;
using Data.Entities.Propuestas;
using Data.Entities.RedSocial;
using Data.Entities.Transpariencia;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Candidatos
{
    public class Candidato : CrudEntities
    {

        //Candidato: Representa un candidato y contiene información personal, afiliación política, experiencia, propuestas, posiciones, transparencia, enlaces de redes sociales, apoyos y eventos.
        [Key]
        public long IdCandidato { get; set; }
        [Required]
        [MaxLength(100)]
        public string NombreCandidato { get; set; } = "";
        public int Edad { get; set; }
        public string FotoUrl { get; set; } = "";
        public string LugarDeNacimiento { get; set; } = "";
        public string InformacionDeContacto { get; set; } = "";
        [ForeignKey("Partido")]
        public long IdPartido { get; set; }
        [ForeignKey("Cargo")]
        public long IdCargo { get; set; }
        [ForeignKey("Transpariencia")]
        public long IdTranspariencia { get; set; }
        public virtual Cargo.Cargo? Cargo { get; set; }
        public virtual Partido? Partido { get; set; }
        public virtual Transpariencia.Transpariencia? Transpariencia { get; set; }
        public virtual ICollection<Propuesta>? Propuestas { get; set; }
        public virtual ICollection<Experiencia.Trayectoria>? Experiencias { get; set; }
        public virtual ICollection<Pocision.Pocision>? Pocisiones { get; set; }
        public virtual ICollection<RedSocial.RedSocial>? RedesSociales { get; set; }
        public virtual ICollection<Apoyo.Apoyo>? Apoyos { get; set; }
        public virtual ICollection<Evento.Evento>? Eventos { get; set; }











    }
}
