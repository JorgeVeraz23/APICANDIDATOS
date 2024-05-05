using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Globalization;

using PuntoDeVentaData.Entities.Security;
using PuntoDeVentaData.Entities.Parameters;
using PuntoDeVentaData.Entities.Templates;
using PuntoDeVentaData.Entities.Enumerators;
namespace LinkQuality.Data.Repository.UtilitiesRepository
{
    public static class UtilitiesRepository
    {
        public static void ConfigurationTablesUsersAndRols(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
             .ToTable("Users", "SEG")
             .Property(u => u.Activo)
             .HasDefaultValue(true);

            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsersToken", "SEG");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role", "SEG");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "SEG");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "SEG");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "SEG");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "SEG");
        }
        public static void ConfigurationDefaultRoles(this ModelBuilder modelBuilder)
        {

            //*******************
            //Datos Iniciales
            //*******************
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole()
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Tester",
                    ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    NormalizedName = "Tester".ToUpper(),

                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "Test@apptelink.com",
                    IpRegister = "::1",

                }
                );
            modelBuilder.Entity<ApplicationRole>().HasData(
                   new ApplicationRole()
                   {
                       Id = "2c5e174e-3b0e-446f-86af-483d56fd7211",
                       Name = "Administrador",
                       ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7211",
                       NormalizedName = "Administrador".ToUpper(),

                       Active = true,
                       DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                       UserRegister = "Test@apptelink.com",
                       IpRegister = "::1",

                   }
                   );
            modelBuilder.Entity<ApplicationRole>().HasData(
                   new ApplicationRole()
                   {
                       Id = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                       Name = "Inspector",
                       ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                       NormalizedName = "Inspector".ToUpper(),

                       Active = true,
                       DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                       UserRegister = "Test@apptelink.com",
                       IpRegister = "::1",

                   }
                   );

        }

        public static void ConfigurationDefaultDataUsuario(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
               new ApplicationUser()
               {
                   Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                   UserName = "test@apptelink.com",
                   Email = "test@apptelink.com",
                   NormalizedUserName = "test@apptelink.com".ToUpper(),
                   NormalizedEmail = "test@apptelink.com".ToUpper(),
                   PasswordHash = "AQAAAAIAAYagAAAAEHLlVesXJKPW6QD+gMA/K7PG8CJYA/dJpiq2vDV848iNpsIzV1A2GVf4h4cQFkQ0Ew==",//Test2024!
                   EmailConfirmed = true,
                   LockoutEnabled = true,
                   PhoneNumberConfirmed = true,
                   SecurityStamp = "b7a8260b-c0cd-4d2b-bb5c-553774e025f2",
                   Activo = true,
                   FechaRegistro = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   UsuarioRegistro = "CreacionInicial",
                   IpRegistro = "::1",
                   ConcurrencyStamp = "96e24203-c5ab-4493-a657-22c53f5b5659",

               }
                ); ;

        }
        public static void ConfigurationDefaultDataUserRol(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                  new IdentityUserRole<string>()
                  {
                      RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210", // 2c5e174e-3b0e-446f-86af-483d56fd7210
                      UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9" // 8e445865-a24d-4543-a6c6-9443d048cdb9
                  }
                  );
        }
        public static void ConfigurationDefaultDataTipoParametro(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParameterType>().HasData(
                new ParameterType()
                {
                    IdTypeParameter = 1,
                    Name = "Configuracion envio Correo",
                    Icon = "",
                    Orden = 1,

                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "::1",
                });
            modelBuilder.Entity<ParameterType>().HasData(
                new ParameterType()
                {
                    IdTypeParameter = 2,
                    Name = "Parametros Geolocalización",
                    Icon = "",
                    Orden = 2,

                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "::1",
                });
        }
        public static void ConfigurationDefaultDataParametros(this ModelBuilder modelBuilder)
        {
            //***********************************
            // PARAMETROS
            //***********************************

            modelBuilder.Entity<Parameters>().HasData(
                new Parameters()
                {
                    IdParameter = 1,
                    EnumParameter = EnumParams.SendMailSmtp,
                    Value = "smtp.office365.com",
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "::1",
                    IdTypeParameter = 1,

                });

            modelBuilder.Entity<Parameters>().HasData(
               new Parameters()
               {
                   IdParameter = 2,
                   EnumParameter = EnumParams.SendMailPort,
                   Value = "587",
                   Active = true,
                   DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   UserRegister = "CreacionInicial",
                   IpRegister = "::1",
                   IdTypeParameter = 1,

               });

            modelBuilder.Entity<Parameters>().HasData(
      new Parameters()
      {
          IdParameter = 3,
          EnumParameter = EnumParams.SendMailUser,
          Value = "notificaciones@citikold.com",
          Active = true,
          DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
          UserRegister = "CreacionInicial",
          IpRegister = "::1",
          IdTypeParameter = 1,

      });
            modelBuilder.Entity<Parameters>().HasData(
             new Parameters()
             {
                 IdParameter = 4,
                 EnumParameter = EnumParams.SendMailPassword,
                 Value = "C1t1k0ld*+",
                 Active = true,
                 DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                 UserRegister = "CreacionInicial",
                 IpRegister = "::1",
                 IdTypeParameter = 1,
             });
        }

        public static void ConfigurationDefaultDataMenu(this ModelBuilder modelBuilder)
        {
            //***********************************
            // MENU
            //***********************************

            modelBuilder.Entity<Menu>().HasData(
                new Menu()
                {
                    IdMenu = 1,
                    IdMenuFather = null,
                    Nivel = 1,
                    Orden = 1,
                    Code = "01",
                    Name = "Catálogos",
                    Description = "Catálogos",
                    Controller = "-",
                    IsVisible = true,
                    IsCreate = false,
                    IsEdit = false,
                    IsDownloadExcel = false,
                    IsPrint = false,
                    IsProcess = false,
                    IsSendEmail = false,
                    IsApproved = false,
                    ColorRef = "-",
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "Ip:..",

                });
            modelBuilder.Entity<Menu>().HasData(
               new Menu()
               {
                   IdMenu = 2,
                   IdMenuFather = 1,
                   Nivel = 2,
                   Orden = 1,
                   Code = "01.001",
                   Name = "Parámetros",
                   Description = "Parámetros",
                   Controller = "Parametro",
                   View = "Index",
                   RelativeURL = "~/Parametro/Index",
                   IsVisible = true,
                   IsCreate = true,
                   IsEdit = true,
                   IsPrint = false,
                   IsProcess = false,
                   IsSendEmail = false,
                   IsApproved = false,
                   ColorRef = "-",
                   Active = true,
                   DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   UserRegister = "CreacionInicial",
                   IpRegister = "Ip:..",

               });
            modelBuilder.Entity<Menu>().HasData(
              new Menu()
              {
                  IdMenu = 3,
                  IdMenuFather = 1,
                  Nivel = 2,
                  Orden = 2,
                  Code = "01.002",
                  Name = "Empresa",
                  Description = "Empresa",
                  Controller = "Empresa",
                  View = "Index",
                  RelativeURL = "~/Empresa/Index",
                  IsVisible = true,
                  IsCreate = false,
                  IsEdit = true,
                  IsDownloadExcel = false,
                  IsPrint = false,
                  IsProcess = false,
                  IsSendEmail = false,
                  IsApproved = false,
                  ColorRef = "-",
                  Active = true,
                  DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                  UserRegister = "CreacionInicial",
                  IpRegister = "Ip:..",

              });
            modelBuilder.Entity<Menu>().HasData(
                new Menu()
                {
                    IdMenu = 4,
                    IdMenuFather = null,
                    Nivel = 1,
                    Orden = 2,
                    Code = "02",
                    Name = "Mantenimiento",
                    Description = string.Empty,
                    Icon = "GlobeAltIcon",
                    Controller = "dashboard",
                    View = "paises",
                    IsVisible = true,
                    IsCreate = false,
                    IsEdit = false,
                    IsDownloadExcel = false,
                    IsPrint = false,
                    IsProcess = false,
                    IsSendEmail = false,
                    IsApproved = false,
                    ColorRef = "-",
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "0.0.0.0",

                });
            modelBuilder.Entity<Menu>().HasData(
                new Menu()
                {
                    IdMenu = 5,
                    IdMenuFather = 4,
                    Nivel = 2,
                    Orden = 1,
                    Code = "02.01",
                    Name = "Parametrizaciones",
                    Description = string.Empty,
                    Icon = "WrenchScrewdriverIcon",
                    Controller = string.Empty,
                    View = string.Empty,
                    IsVisible = true,
                    IsCreate = false,
                    IsEdit = false,
                    IsDownloadExcel = false,
                    IsPrint = false,
                    IsProcess = false,
                    IsSendEmail = false,
                    IsApproved = false,
                    ColorRef = "-",
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "0.0.0.0",

                });
            modelBuilder.Entity<Menu>().HasData(
                new Menu()
                {
                    IdMenu = 6,
                    IdMenuFather = 4,
                    Nivel = 2,
                    Orden = 2,
                    Code = "02.02",
                    Name = "Localización",
                    Description = string.Empty,
                    Icon = "GlobeAltIcon",
                    Controller = string.Empty,
                    View = string.Empty,
                    IsVisible = true,
                    IsCreate = false,
                    IsEdit = false,
                    IsDownloadExcel = false,
                    IsPrint = false,
                    IsProcess = false,
                    IsSendEmail = false,
                    IsApproved = false,
                    ColorRef = "-",
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "0.0.0.0",

                });
            modelBuilder.Entity<Menu>().HasData(
                new Menu()
                {
                    IdMenu = 7,
                    IdMenuFather = 6,
                    Nivel = 3,
                    Orden = 1,
                    Code = "02.02.001",
                    Icon = "GlobeAltIcon",
                    Name = "Países",
                    Description = "Catálogos de Países",
                    Controller = "dashboard",
                    View = "paises",
                    IsVisible = true,
                    IsCreate = false,
                    IsEdit = false,
                    IsDownloadExcel = false,
                    IsPrint = false,
                    IsProcess = false,
                    IsSendEmail = false,
                    IsApproved = false,
                    ColorRef = "-",
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "0.0.0.0",
                });

        }
        public static void ConfigurationDefaultDataMenuRole(this ModelBuilder modelBuilder)
        {
            //***********************************
            // MENU ROLE
            //***********************************
            modelBuilder.Entity<MenuRole>().HasData(
                new MenuRole()
                {
                    IdMenuRole = 1,
                    IdRole = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    IdMenu = 4,
                    IsVisible = true,
                    IsCreate = false,
                    IsEdit = false,
                    IsSendEmail = false,
                    IsPrint = false,
                    IsDownloadExcel = false,
                    IsProcess = false,
                    IsApproved = false,
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "0.0.0.0",

                });
            modelBuilder.Entity<MenuRole>().HasData(
                new MenuRole()
                {
                    IdMenuRole = 2,
                    IdRole = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    IdMenu = 6,
                    IsVisible = true,
                    IsCreate = false,
                    IsEdit = false,
                    IsSendEmail = false,
                    IsPrint = false,
                    IsDownloadExcel = false,
                    IsProcess = false,
                    IsApproved = false,
                    Active = true,
                    DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "0.0.0.0",

                });
            modelBuilder.Entity<MenuRole>().HasData(
                new MenuRole()
                {
                    IdMenuRole = 3,
                    IdRole = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    IdMenu = 7,
                    IsVisible = true,
                    IsCreate = true,
                    IsEdit = true,
                    IsSendEmail = false,
                    IsPrint = false,
                    IsDownloadExcel = true,
                    IsProcess = false,
                    IsApproved = false,
                    Active = true,
                    DateRegister = DateTime.ParseExact("23/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRegister = "CreacionInicial",
                    IpRegister = "0.0.0.0",

                });
        }
        public static void ConfigurationDefaultDataEmailTemplate(this ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<EmailTemplate>().HasData(
            new EmailTemplate()
            {
                IdEmailTemplate = 1,
                Enumerator = "recuperarContrasenia",
                Subject = "[Nomina PRO] Reseteo de contraseña",
                Body = "Saludos cordiales,<br>Estimado {0} su contraseña ha sido reestablecido<br>Nueva contraseña:{1}<br>Gracias por su atención.",
                Params = "",
                Active = true,
                DateRegister = DateTime.ParseExact("17/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                UserRegister = "Test@apptelink.com",
                IpRegister = "::1",

            }
                        );
        }

        //public static void ConfigurationDefaultDataFormStage(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<>().HasData(
        //          new FormStage()
        //          {
        //              IdFormStage = 1,
        //              Active = true,
        //              DateRegister = DateTime.ParseExact("06/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
        //              UserRegister = "CreacionInicial",
        //              IpRegister = "::1",
        //              Name = "PRELIMINAR",
        //              Order = 1,

        //          }
        //    );


        //    modelBuilder.Entity<FormStage>().HasData(
        //          new FormStage()
        //          {
        //              IdFormStage = 2,
        //              Active = true,
        //              DateRegister = DateTime.ParseExact("06/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
        //              UserRegister = "CreacionInicial",
        //              IpRegister = "::1",
        //              Name = "FINAL",
        //              Order = 2,

        //          }
        //    );


        //}

        //public static void ConfigurationDefaultDataInspectionForm(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<InspectionForm>().HasData(
        //          new InspectionForm()
        //          {
        //              IdInspectionForm = 1,
        //              Active = true,
        //              DateRegister = DateTime.ParseExact("06/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
        //              UserRegister = "CreacionInicial",
        //              IpRegister = "::1",
        //              NameES = "CONTENEDOR BANANO",

        //          }
        //    );


        //    modelBuilder.Entity<InspectionForm>().HasData(
        //          new InspectionForm()
        //          {
        //              IdInspectionForm = 2,
        //              Active = true,
        //              DateRegister = DateTime.ParseExact("06/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
        //              UserRegister = "CreacionInicial",
        //              IpRegister = "::1",
        //              NameES = "CONTENEDOR PLÁTANO",

        //          }
        //    );


        //    modelBuilder.Entity<InspectionForm>().HasData(
        //          new InspectionForm()
        //          {
        //              IdInspectionForm = 3,
        //              Active = true,
        //              DateRegister = DateTime.ParseExact("06/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
        //              UserRegister = "CreacionInicial",
        //              IpRegister = "::1",
        //              NameES = "EMBARQUES",

        //          }
        //    );


        //    modelBuilder.Entity<InspectionForm>().HasData(
        //          new InspectionForm()
        //          {
        //              IdInspectionForm = 4,
        //              Active = true,
        //              DateRegister = DateTime.ParseExact("06/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
        //              UserRegister = "CreacionInicial",
        //              IpRegister = "::1",
        //              NameES = "GANCHOS",

        //          }
        //    );


        //    modelBuilder.Entity<InspectionForm>().HasData(
        //          new InspectionForm()
        //          {
        //              IdInspectionForm = 5,
        //              Active = true,
        //              DateRegister = DateTime.ParseExact("06/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture),
        //              UserRegister = "CreacionInicial",
        //              IpRegister = "::1",
        //              NameES = "VACIOS",

        //          }
        //    );

        //}
    }
}
