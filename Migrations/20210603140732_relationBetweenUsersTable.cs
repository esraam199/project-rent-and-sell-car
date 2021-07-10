using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsApi.Migrations
{
    public partial class relationBetweenUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserID",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_AspNetUserID",
                table: "User",
                column: "AspNetUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers_AspNetUserID",
                table: "User",
                column: "AspNetUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers_AspNetUserID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AspNetUserID",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserID",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
