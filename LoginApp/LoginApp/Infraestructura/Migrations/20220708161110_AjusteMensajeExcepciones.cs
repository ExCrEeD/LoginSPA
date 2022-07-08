using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginApp.Infraestructura.Migrations
{
    public partial class AjusteMensajeExcepciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExcepcionAutenticacion",
                table: "IniciosDeSesion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcepcionAutenticacion",
                table: "IniciosDeSesion");
        }
    }
}
