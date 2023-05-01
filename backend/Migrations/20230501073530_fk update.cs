using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_api.Migrations
{
    /// <inheritdoc />
    public partial class fkupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_RootUsers_UserId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_User_Id",
                table: "Articles",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_RootUsers_User_Id",
                table: "Articles",
                column: "User_Id",
                principalTable: "RootUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_RootUsers_User_Id",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_User_Id",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_RootUsers_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "RootUsers",
                principalColumn: "Id");
        }
    }
}
