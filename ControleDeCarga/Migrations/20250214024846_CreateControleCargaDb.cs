using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeCarga.Migrations
{
    /// <inheritdoc />
    public partial class CreateControleCargaDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaVeiculo",
                columns: table => new
                {
                    Id = table.Column<char>(type: "char(3)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaVeiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusCarregamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusCarregamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transportadoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportadoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioHierarquia",
                columns: table => new
                {
                    Id = table.Column<char>(type: "char(3)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioHierarquia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motoristas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Cnh = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TransportadoraId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoristas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motoristas_Transportadoras_TransportadoraId",
                        column: x => x.TransportadoraId,
                        principalTable: "Transportadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(7)", nullable: false),
                    CategoriaId = table.Column<char>(type: "char(3)", nullable: false),
                    TransportadoraId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_CategoriaVeiculo_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriaVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Veiculos_Transportadoras_TransportadoraId",
                        column: x => x.TransportadoraId,
                        principalTable: "Transportadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Senha = table.Column<string>(type: "varbinary(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TipoUsuarioId = table.Column<char>(type: "char(3)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false),
                    TentativasAcesso = table.Column<int>(type: "int", nullable: false),
                    PrimeiroAcesso = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_UsuarioHierarquia_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "UsuarioHierarquia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentoCargas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    MotoristaId = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentoCargas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanejamentoCargas_Motoristas_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motoristas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PlanejamentoCargas_StatusCarregamento_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusCarregamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PlanejamentoCargas_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoMovimentacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargaId = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    StatusAnterior = table.Column<int>(type: "int", nullable: false),
                    StatusAtual = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoMovimentacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoMovimentacao_PlanejamentoCargas_CargaId",
                        column: x => x.CargaId,
                        principalTable: "PlanejamentoCargas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoMovimentacao_CargaId",
                table: "HistoricoMovimentacao",
                column: "CargaId");

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_TransportadoraId",
                table: "Motoristas",
                column: "TransportadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCargas_MotoristaId",
                table: "PlanejamentoCargas",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCargas_StatusId",
                table: "PlanejamentoCargas",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoCargas_VeiculoId",
                table: "PlanejamentoCargas",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_CategoriaId",
                table: "Veiculos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_TransportadoraId",
                table: "Veiculos",
                column: "TransportadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoMovimentacao");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "PlanejamentoCargas");

            migrationBuilder.DropTable(
                name: "UsuarioHierarquia");

            migrationBuilder.DropTable(
                name: "Motoristas");

            migrationBuilder.DropTable(
                name: "StatusCarregamento");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "CategoriaVeiculo");

            migrationBuilder.DropTable(
                name: "Transportadoras");
        }
    }
}
