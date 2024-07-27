using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class uno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SEG");

            migrationBuilder.CreateTable(
                name: "ApplicationVersions",
                columns: table => new
                {
                    IdApplicationVersion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlatForm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationVersions", x => x.IdApplicationVersion);
                });

            migrationBuilder.CreateTable(
                name: "AuditoryAccesses",
                columns: table => new
                {
                    IdAuditoryAccess = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DateAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoryAccesses", x => x.IdAuditoryAccess);
                });

            migrationBuilder.CreateTable(
                name: "BucketFiles",
                columns: table => new
                {
                    IdBucketFile = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TempFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sync = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketFiles", x => x.IdBucketFile);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    IdCargo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.IdCargo);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    IdEmailTemplate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enumerator = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Body = table.Column<string>(type: "ntext", nullable: false),
                    Params = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.IdEmailTemplate);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    IdLog = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestTraceIdentifier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackStrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InnerException = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plataform = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ambiente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.IdLog);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    IdMenu = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMenuFather = table.Column<long>(type: "bigint", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    View = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AbsoluteURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RelativeURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ColorRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsCreate = table.Column<bool>(type: "bit", nullable: false),
                    IsEdit = table.Column<bool>(type: "bit", nullable: false),
                    IsSendEmail = table.Column<bool>(type: "bit", nullable: false),
                    IsPrint = table.Column<bool>(type: "bit", nullable: false),
                    IsDownloadExcel = table.Column<bool>(type: "bit", nullable: false),
                    IsDownloadPDF = table.Column<bool>(type: "bit", nullable: false),
                    IsProcess = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.IdMenu);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_IdMenuFather",
                        column: x => x.IdMenuFather,
                        principalTable: "Menus",
                        principalColumn: "IdMenu");
                });

            migrationBuilder.CreateTable(
                name: "ParameterTypes",
                columns: table => new
                {
                    IdTypeParameter = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterTypes", x => x.IdTypeParameter);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    IdPartido = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePartido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.IdPartido);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transparencias",
                columns: table => new
                {
                    IdTranspariencia = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeclaracionesDeBienes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvolucradoEnEscandalos = table.Column<bool>(type: "bit", nullable: false),
                    EvaluacionesDeEtica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transparencias", x => x.IdTranspariencia);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bloqueo = table.Column<bool>(type: "bit", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RutaFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    UsuarioRegistro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegistro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModificacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpEliminacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    IdParameter = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnumParameter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    Comentary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    IdTypeParameter = table.Column<long>(type: "bigint", nullable: false),
                    TypeParameterIdTypeParameter = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.IdParameter);
                    table.ForeignKey(
                        name: "FK_Parameters_ParameterTypes_TypeParameterIdTypeParameter",
                        column: x => x.TypeParameterIdTypeParameter,
                        principalTable: "ParameterTypes",
                        principalColumn: "IdTypeParameter");
                });

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    IdCandidato = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCandidato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    FotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LugarDeNacimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformacionDeContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPartido = table.Column<long>(type: "bigint", nullable: true),
                    IdCargo = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.IdCandidato);
                    table.ForeignKey(
                        name: "FK_Candidatos_Cargos_IdCargo",
                        column: x => x.IdCargo,
                        principalTable: "Cargos",
                        principalColumn: "IdCargo");
                    table.ForeignKey(
                        name: "FK_Candidatos_Partidos_IdPartido",
                        column: x => x.IdPartido,
                        principalTable: "Partidos",
                        principalColumn: "IdPartido");
                });

            migrationBuilder.CreateTable(
                name: "MenuRoles",
                columns: table => new
                {
                    IdMenuRole = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsCreate = table.Column<bool>(type: "bit", nullable: false),
                    IsEdit = table.Column<bool>(type: "bit", nullable: false),
                    IsSendEmail = table.Column<bool>(type: "bit", nullable: false),
                    IsPrint = table.Column<bool>(type: "bit", nullable: false),
                    IsDownloadExcel = table.Column<bool>(type: "bit", nullable: false),
                    IsDownloadPDF = table.Column<bool>(type: "bit", nullable: false),
                    IsProcess = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IdRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdMenu = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRoles", x => x.IdMenuRole);
                    table.ForeignKey(
                        name: "FK_MenuRoles_Menus_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menus",
                        principalColumn: "IdMenu");
                    table.ForeignKey(
                        name: "FK_MenuRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEG",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEG",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacions",
                columns: table => new
                {
                    IdNotification = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JsonUsersDestinatarios = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUserOrigen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userOrigenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacions", x => x.IdNotification);
                    table.ForeignKey(
                        name: "FK_Notificacions_Users_userOrigenId",
                        column: x => x.userOrigenId,
                        principalSchema: "SEG",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "SEG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "SEG",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "SEG",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SEG",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersToken",
                schema: "SEG",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UsersToken_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SEG",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apoyos",
                columns: table => new
                {
                    IdApoyo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDelPartidario = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apoyos", x => x.IdApoyo);
                    table.ForeignKey(
                        name: "FK_Apoyos_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato");
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    IdEvento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.IdEvento);
                    table.ForeignKey(
                        name: "FK_Eventos_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato");
                });

            migrationBuilder.CreateTable(
                name: "Posiciones",
                columns: table => new
                {
                    IdPocision = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tema = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Postura = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posiciones", x => x.IdPocision);
                    table.ForeignKey(
                        name: "FK_Posiciones_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato");
                });

            migrationBuilder.CreateTable(
                name: "Propuestas",
                columns: table => new
                {
                    IdPropuesta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propuestas", x => x.IdPropuesta);
                    table.ForeignKey(
                        name: "FK_Propuestas_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato");
                });

            migrationBuilder.CreateTable(
                name: "RedSocials",
                columns: table => new
                {
                    IdRedsocial = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plataforma = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedSocials", x => x.IdRedsocial);
                    table.ForeignKey(
                        name: "FK_RedSocials_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato");
                });

            migrationBuilder.CreateTable(
                name: "Trayectorias",
                columns: table => new
                {
                    IdTrayectoria = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpRegister = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    IpModification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trayectorias", x => x.IdTrayectoria);
                    table.ForeignKey(
                        name: "FK_Trayectorias_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato");
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "IdEmailTemplate", "Active", "Body", "DateDelete", "DateModification", "DateRegister", "Enumerator", "IpDelete", "IpModification", "IpRegister", "Params", "Subject", "UserDelete", "UserModification", "UserRegister" },
                values: new object[] { 1L, true, "Saludos cordiales,<br>Estimado {0} su contraseña ha sido reestablecido<br>Nueva contraseña:{1}<br>Gracias por su atención.", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "recuperarContrasenia", null, null, "::1", "", "[Nomina PRO] Reseteo de contraseña", null, null, "Test@apptelink.com" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "IdMenu", "AbsoluteURL", "Active", "Code", "ColorRef", "Controller", "DateDelete", "DateModification", "DateRegister", "Description", "Icon", "IdMenuFather", "IpDelete", "IpModification", "IpRegister", "IsApproved", "IsCreate", "IsDownloadExcel", "IsDownloadPDF", "IsEdit", "IsPrint", "IsProcess", "IsSendEmail", "IsVisible", "Name", "Nivel", "Orden", "RelativeURL", "UserDelete", "UserModification", "UserRegister", "View" },
                values: new object[,]
                {
                    { 1L, null, true, "01", "-", "-", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Catálogos", null, null, null, null, "Ip:..", false, false, false, false, false, false, false, false, true, "Catálogos", 1, 1, null, null, null, "CreacionInicial", null },
                    { 4L, null, true, "02", "-", "dashboard", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "GlobeAltIcon", null, null, null, "0.0.0.0", false, false, false, false, false, false, false, false, true, "Mantenimiento", 1, 2, null, null, null, "CreacionInicial", "paises" }
                });

            migrationBuilder.InsertData(
                table: "ParameterTypes",
                columns: new[] { "IdTypeParameter", "Active", "DateDelete", "DateModification", "DateRegister", "Icon", "IpDelete", "IpModification", "IpRegister", "Name", "Orden", "UserDelete", "UserModification", "UserRegister" },
                values: new object[,]
                {
                    { 1L, true, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, "::1", "Configuracion envio Correo", 1, null, null, "CreacionInicial" },
                    { 2L, true, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, "::1", "Parametros Geolocalización", 2, null, null, "CreacionInicial" }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "IdParameter", "Active", "Comentary", "DataType", "DateDelete", "DateModification", "DateRegister", "EnumParameter", "IdTypeParameter", "IpDelete", "IpModification", "IpRegister", "Orden", "TypeParameterIdTypeParameter", "UserDelete", "UserModification", "UserRegister", "Value" },
                values: new object[,]
                {
                    { 1L, true, null, 0, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "SendMailSmtp", 1L, null, null, "::1", 0, null, null, null, "CreacionInicial", "smtp.office365.com" },
                    { 2L, true, null, 0, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "SendMailPort", 1L, null, null, "::1", 0, null, null, null, "CreacionInicial", "587" },
                    { 3L, true, null, 0, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "SendMailUser", 1L, null, null, "::1", 0, null, null, null, "CreacionInicial", "notificaciones@citikold.com" },
                    { 4L, true, null, 0, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "SendMailPassword", 1L, null, null, "::1", 0, null, null, null, "CreacionInicial", "C1t1k0ld*+" }
                });

            migrationBuilder.InsertData(
                schema: "SEG",
                table: "Role",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "DateDelete", "DateModification", "DateRegister", "IpDelete", "IpModification", "IpRegister", "Name", "NormalizedName", "UserDelete", "UserModification", "UserRegister" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", true, "2c5e174e-3b0e-446f-86af-483d56fd7210", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "::1", "Tester", "TESTER", null, null, "Test@apptelink.com" },
                    { "2c5e174e-3b0e-446f-86af-483d56fd7211", true, "2c5e174e-3b0e-446f-86af-483d56fd7211", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "::1", "Administrador", "ADMINISTRADOR", null, null, "Test@apptelink.com" },
                    { "2c5e174e-3b0e-446f-86af-483d56fd7212", true, "2c5e174e-3b0e-446f-86af-483d56fd7212", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "::1", "Inspector", "INSPECTOR", null, null, "Test@apptelink.com" }
                });

            migrationBuilder.InsertData(
                schema: "SEG",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Activo", "Bloqueo", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "FechaEliminacion", "FechaModificacion", "FechaRegistro", "FirstName", "Identificacion", "IpEliminacion", "IpModificacion", "IpRegistro", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RutaFoto", "SecurityStamp", "TwoFactorEnabled", "UserName", "UsuarioEliminacion", "UsuarioModificacion", "UsuarioRegistro" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, true, false, "96e24203-c5ab-4493-a657-22c53f5b5659", null, "test@apptelink.com", true, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "::1", null, true, null, "TEST@APPTELINK.COM", "TEST@APPTELINK.COM", "AQAAAAIAAYagAAAAEHLlVesXJKPW6QD+gMA/K7PG8CJYA/dJpiq2vDV848iNpsIzV1A2GVf4h4cQFkQ0Ew==", null, true, null, "b7a8260b-c0cd-4d2b-bb5c-553774e025f2", false, "test@apptelink.com", null, null, "CreacionInicial" });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "IdMenuRole", "Active", "DateDelete", "DateModification", "DateRegister", "IdMenu", "IdRole", "IpDelete", "IpModification", "IpRegister", "IsApproved", "IsCreate", "IsDownloadExcel", "IsDownloadPDF", "IsEdit", "IsPrint", "IsProcess", "IsSendEmail", "IsVisible", "RoleId", "UserDelete", "UserModification", "UserRegister" },
                values: new object[] { 1L, true, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, "2c5e174e-3b0e-446f-86af-483d56fd7210", null, null, "0.0.0.0", false, false, false, false, false, false, false, false, true, null, null, null, "CreacionInicial" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "IdMenu", "AbsoluteURL", "Active", "Code", "ColorRef", "Controller", "DateDelete", "DateModification", "DateRegister", "Description", "Icon", "IdMenuFather", "IpDelete", "IpModification", "IpRegister", "IsApproved", "IsCreate", "IsDownloadExcel", "IsDownloadPDF", "IsEdit", "IsPrint", "IsProcess", "IsSendEmail", "IsVisible", "Name", "Nivel", "Orden", "RelativeURL", "UserDelete", "UserModification", "UserRegister", "View" },
                values: new object[,]
                {
                    { 2L, null, true, "01.001", "-", "Parametro", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Parámetros", null, 1L, null, null, "Ip:..", false, true, false, false, true, false, false, false, true, "Parámetros", 2, 1, "~/Parametro/Index", null, null, "CreacionInicial", "Index" },
                    { 3L, null, true, "01.002", "-", "Empresa", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empresa", null, 1L, null, null, "Ip:..", false, false, false, false, true, false, false, false, true, "Empresa", 2, 2, "~/Empresa/Index", null, null, "CreacionInicial", "Index" },
                    { 5L, null, true, "02.01", "-", "", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "WrenchScrewdriverIcon", 4L, null, null, "0.0.0.0", false, false, false, false, false, false, false, false, true, "Parametrizaciones", 2, 1, null, null, null, "CreacionInicial", "" },
                    { 6L, null, true, "02.02", "-", "", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "GlobeAltIcon", 4L, null, null, "0.0.0.0", false, false, false, false, false, false, false, false, true, "Localización", 2, 2, null, null, null, "CreacionInicial", "" }
                });

            migrationBuilder.InsertData(
                schema: "SEG",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "IdMenuRole", "Active", "DateDelete", "DateModification", "DateRegister", "IdMenu", "IdRole", "IpDelete", "IpModification", "IpRegister", "IsApproved", "IsCreate", "IsDownloadExcel", "IsDownloadPDF", "IsEdit", "IsPrint", "IsProcess", "IsSendEmail", "IsVisible", "RoleId", "UserDelete", "UserModification", "UserRegister" },
                values: new object[] { 2L, true, null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, "2c5e174e-3b0e-446f-86af-483d56fd7210", null, null, "0.0.0.0", false, false, false, false, false, false, false, false, true, null, null, null, "CreacionInicial" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "IdMenu", "AbsoluteURL", "Active", "Code", "ColorRef", "Controller", "DateDelete", "DateModification", "DateRegister", "Description", "Icon", "IdMenuFather", "IpDelete", "IpModification", "IpRegister", "IsApproved", "IsCreate", "IsDownloadExcel", "IsDownloadPDF", "IsEdit", "IsPrint", "IsProcess", "IsSendEmail", "IsVisible", "Name", "Nivel", "Orden", "RelativeURL", "UserDelete", "UserModification", "UserRegister", "View" },
                values: new object[] { 7L, null, true, "02.02.001", "-", "dashboard", null, null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Catálogos de Países", "GlobeAltIcon", 6L, null, null, "0.0.0.0", false, false, false, false, false, false, false, false, true, "Países", 3, 1, null, null, null, "CreacionInicial", "paises" });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "IdMenuRole", "Active", "DateDelete", "DateModification", "DateRegister", "IdMenu", "IdRole", "IpDelete", "IpModification", "IpRegister", "IsApproved", "IsCreate", "IsDownloadExcel", "IsDownloadPDF", "IsEdit", "IsPrint", "IsProcess", "IsSendEmail", "IsVisible", "RoleId", "UserDelete", "UserModification", "UserRegister" },
                values: new object[] { 3L, true, null, null, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, "2c5e174e-3b0e-446f-86af-483d56fd7210", null, null, "0.0.0.0", false, true, true, false, true, false, false, false, true, null, null, null, "CreacionInicial" });

            migrationBuilder.CreateIndex(
                name: "IX_Apoyos_IdCandidato",
                table: "Apoyos",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_IdCargo",
                table: "Candidatos",
                column: "IdCargo");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_IdPartido",
                table: "Candidatos",
                column: "IdPartido");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_IdCandidato",
                table: "Eventos",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoles_IdMenu",
                table: "MenuRoles",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoles_RoleId",
                table: "MenuRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_IdMenuFather",
                table: "Menus",
                column: "IdMenuFather");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacions_userOrigenId",
                table: "Notificacions",
                column: "userOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_TypeParameterIdTypeParameter",
                table: "Parameters",
                column: "TypeParameterIdTypeParameter");

            migrationBuilder.CreateIndex(
                name: "IX_Posiciones_IdCandidato",
                table: "Posiciones",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_Propuestas_IdCandidato",
                table: "Propuestas",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_RedSocials_IdCandidato",
                table: "RedSocials",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "SEG",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "SEG",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Trayectorias_IdCandidato",
                table: "Trayectorias",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "SEG",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "SEG",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "SEG",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "SEG",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "SEG",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apoyos");

            migrationBuilder.DropTable(
                name: "ApplicationVersions");

            migrationBuilder.DropTable(
                name: "AuditoryAccesses");

            migrationBuilder.DropTable(
                name: "BucketFiles");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "Notificacions");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Posiciones");

            migrationBuilder.DropTable(
                name: "Propuestas");

            migrationBuilder.DropTable(
                name: "RedSocials");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Transparencias");

            migrationBuilder.DropTable(
                name: "Trayectorias");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UsersToken",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "ParameterTypes");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Partidos");
        }
    }
}
