using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class ErrorDeleteNota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Estudiantes_IdEstudiante",
                table: "Notas");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Estudiantes_IdEstudiante",
                table: "Notas",
                column: "IdEstudiante",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Estudiantes_IdEstudiante",
                table: "Notas");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Estudiantes_IdEstudiante",
                table: "Notas",
                column: "IdEstudiante",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
