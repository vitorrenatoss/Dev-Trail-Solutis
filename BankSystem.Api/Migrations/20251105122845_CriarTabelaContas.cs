namespace BankSystem.Api.Migrations;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


/// <inheritdoc />
public partial class CriarTabelaContas : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Contas",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NumeroConta = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                Tipo = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Contas", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Contas");
    }
}
