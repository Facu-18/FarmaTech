using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmaTech.BD.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDerDefinitivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngresosMercaderia_Droguerias_IdDrogueria",
                table: "IngresosMercaderia");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasPedido_Droguerias_IdDrogueria",
                table: "NotasPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasPedido_Productos_IdProducto",
                table: "NotasPedido");

            migrationBuilder.AlterColumn<int>(
                name: "IdProducto",
                table: "NotasPedido",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdDrogueria",
                table: "NotasPedido",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdDrogueria",
                table: "IngresosMercaderia",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Codigo",
                table: "Productos",
                column: "Codigo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IngresosMercaderia_Droguerias_IdDrogueria",
                table: "IngresosMercaderia",
                column: "IdDrogueria",
                principalTable: "Droguerias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasPedido_Droguerias_IdDrogueria",
                table: "NotasPedido",
                column: "IdDrogueria",
                principalTable: "Droguerias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasPedido_Productos_IdProducto",
                table: "NotasPedido",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngresosMercaderia_Droguerias_IdDrogueria",
                table: "IngresosMercaderia");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasPedido_Droguerias_IdDrogueria",
                table: "NotasPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasPedido_Productos_IdProducto",
                table: "NotasPedido");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Codigo",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "IdProducto",
                table: "NotasPedido",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdDrogueria",
                table: "NotasPedido",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdDrogueria",
                table: "IngresosMercaderia",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IngresosMercaderia_Droguerias_IdDrogueria",
                table: "IngresosMercaderia",
                column: "IdDrogueria",
                principalTable: "Droguerias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotasPedido_Droguerias_IdDrogueria",
                table: "NotasPedido",
                column: "IdDrogueria",
                principalTable: "Droguerias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotasPedido_Productos_IdProducto",
                table: "NotasPedido",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
