using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaBiblioteca_Projeto_Autoral.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaEhAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCarrinhos_Carrinhos_CarrinhoId",
                table: "ItemCarrinhos");

            migrationBuilder.DropIndex(
                name: "IX_ItemCarrinhos_CarrinhoId",
                table: "ItemCarrinhos");

            migrationBuilder.AddColumn<bool>(
                name: "EhAdministrador",
                table: "Usuarios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CarrinhoId",
                table: "ItemCarrinhos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EhAdministrador",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "CarrinhoId",
                table: "ItemCarrinhos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCarrinhos_CarrinhoId",
                table: "ItemCarrinhos",
                column: "CarrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCarrinhos_Carrinhos_CarrinhoId",
                table: "ItemCarrinhos",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "Id");
        }
    }
}
