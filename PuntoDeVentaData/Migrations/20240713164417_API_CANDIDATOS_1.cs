using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class API_CANDIDATOS_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Transpariencias",
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
                    table.PrimaryKey("PK_Transpariencias", x => x.IdTranspariencia);
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
                    IdPartido = table.Column<long>(type: "bigint", nullable: false),
                    IdCargo = table.Column<long>(type: "bigint", nullable: false),
                    IdTranspariencia = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "IdCargo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidatos_Partidos_IdPartido",
                        column: x => x.IdPartido,
                        principalTable: "Partidos",
                        principalColumn: "IdPartido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidatos_Transpariencias_IdTranspariencia",
                        column: x => x.IdTranspariencia,
                        principalTable: "Transpariencias",
                        principalColumn: "IdTranspariencia",
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
                    IdCandidato = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Cascade);
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
                    IdCandidato = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pocision",
                columns: table => new
                {
                    IdPocision = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tema = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Postura = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Pocision", x => x.IdPocision);
                    table.ForeignKey(
                        name: "FK_Pocision_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Cascade);
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
                    IdCandidato = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedSocials",
                columns: table => new
                {
                    IdResocial = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plataforma = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_RedSocials", x => x.IdResocial);
                    table.ForeignKey(
                        name: "FK_RedSocials_Candidatos_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidatos",
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trayectorias",
                columns: table => new
                {
                    IdTrayectoria = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IdCandidato = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Candidatos_IdTranspariencia",
                table: "Candidatos",
                column: "IdTranspariencia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_IdCandidato",
                table: "Eventos",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_Pocision_IdCandidato",
                table: "Pocision",
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
                name: "IX_Trayectorias_IdCandidato",
                table: "Trayectorias",
                column: "IdCandidato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apoyos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Pocision");

            migrationBuilder.DropTable(
                name: "Propuestas");

            migrationBuilder.DropTable(
                name: "RedSocials");

            migrationBuilder.DropTable(
                name: "Trayectorias");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Transpariencias");
        }
    }
}
