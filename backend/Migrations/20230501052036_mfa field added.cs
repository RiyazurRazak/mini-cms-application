using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_api.Migrations
{
    /// <inheritdoc />
    public partial class mfafieldadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isMfaEnabled",
                table: "RootUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMfaEnabled",
                table: "RootUsers");
        }
    }
}
