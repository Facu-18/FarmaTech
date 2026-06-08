using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmaTech.BD.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Droguerias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanalContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Droguerias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleadas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpleada = table.Column<int>(type: "int", nullable: false),
                    EmpleadaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleadas_EmpleadaId",
                        column: x => x.EmpleadaId,
                        principalTable: "Empleadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngresosMercaderia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    IdDrogueria = table.Column<int>(type: "int", nullable: false),
                    DrogueriaId = table.Column<int>(type: "int", nullable: false),
                    IdEmpleada = table.Column<int>(type: "int", nullable: false),
                    EmpleadaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosMercaderia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosMercaderia_Droguerias_DrogueriaId",
                        column: x => x.DrogueriaId,
                        principalTable: "Droguerias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngresosMercaderia_Empleadas_EmpleadaId",
                        column: x => x.EmpleadaId,
                        principalTable: "Empleadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngresosMercaderia_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotasPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    IdDrogueria = table.Column<int>(type: "int", nullable: false),
                    DrogueriaId = table.Column<int>(type: "int", nullable: false),
                    IdEmpleada = table.Column<int>(type: "int", nullable: false),
                    EmpleadaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotasPedido_Droguerias_DrogueriaId",
                        column: x => x.DrogueriaId,
                        principalTable: "Droguerias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotasPedido_Empleadas_EmpleadaId",
                        column: x => x.EmpleadaId,
                        principalTable: "Empleadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotasPedido_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVenta = table.Column<int>(type: "int", nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosMercaderia_DrogueriaId",
                table: "IngresosMercaderia",
                column: "DrogueriaId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosMercaderia_EmpleadaId",
                table: "IngresosMercaderia",
                column: "EmpleadaId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosMercaderia_ProductoId",
                table: "IngresosMercaderia",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasPedido_DrogueriaId",
                table: "NotasPedido",
                column: "DrogueriaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasPedido_EmpleadaId",
                table: "NotasPedido",
                column: "EmpleadaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasPedido_ProductoId",
                table: "NotasPedido",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_EmpleadaId",
                table: "Ventas",
                column: "EmpleadaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "IngresosMercaderia");

            migrationBuilder.DropTable(
                name: "NotasPedido");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Droguerias");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Empleadas");
        }
    }
}
