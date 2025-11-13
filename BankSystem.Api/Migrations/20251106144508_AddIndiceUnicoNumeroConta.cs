using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIndiceUnicoNumeroConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contas_NumeroConta",
                table: "Contas",
                column: "NumeroConta",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contas_NumeroConta",
                table: "Contas");
        }
    }
}
