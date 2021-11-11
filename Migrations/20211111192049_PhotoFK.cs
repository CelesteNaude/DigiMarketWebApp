using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiMarketWebApp.Migrations
{
    public partial class PhotoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_WebAppUserId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_WebAppUserId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "WebAppUserId",
                table: "Photo");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Photo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_Id",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_Id",
                table: "Photo",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_Id",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_Id",
                table: "Photo");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebAppUserId",
                table: "Photo",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_WebAppUserId",
                table: "Photo",
                column: "WebAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_WebAppUserId",
                table: "Photo",
                column: "WebAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
