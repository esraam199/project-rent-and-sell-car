using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsApi.Migrations
{
    public partial class addFKforASPNETUSERInUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserID",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUserID",
                table: "User");
        }
    }
}
