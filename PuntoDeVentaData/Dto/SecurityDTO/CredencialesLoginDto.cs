using PuntoDeVentaData.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.SecurityDTO
{
   

    public class CredencialesRegistroDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string RolName { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        

    }

    public class CredencialesRegistroExternoDTO : CredencialesRegistroDTO
    {
        public string DataBaseName { get; set; } = string.Empty;
    }

    public class CredencialesDTO
    {
        [Required]
        public string UserName { get; set;}
        [Required]
        public string Password { get; set; } = string.Empty;

    }



    public class CredencialesLoginDTO : CredencialesDTO
    {
        [JsonIgnore]
        public string Role { get; set; } = "Empleado";
    }

    public class CredencialesLogOutDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
    }

    public class UserDataDTO : CrudEntities
    {
        public string Identificacion { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Bloqueo { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string NormalizedUserName { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;  
        public string rolName {  get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public object PhoneNumber { get; set; } = string.Empty;
        public bool PhoneNumberConfirmed { get; set; }
    }

    public class RecuperarContraseniaDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

    }

    public class NotificarInspeccionDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string TypeInspection { get; set; } = string.Empty;
        public string NombreInspection { get; set; } = string.Empty;
    }

    public class RegistrarUsuarioDto
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RolName { get; set; } = "INSPECTOR";
        public string Identificacion { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }


    }

    public class ItemUsuarioSelectorDTO
    {
        public string IdUsuario { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string RolName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

    }

    public class ItemUsuarioDTO : ItemUsuarioSelectorDTO
    {
        public string? Description { get; set; }
        public string? RutaFoto { get; set; }
        public bool Bloqueo { get; set; }
        public bool EmailConfirmed { get; set; }
        public object PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}
