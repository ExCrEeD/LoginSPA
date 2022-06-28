using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginApp.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IniciosDeSesion",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    AccesToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IniciosDeSesion", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IniciosDeSesion");
        }
    }
}
