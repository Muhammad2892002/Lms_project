using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms_project.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Add_Students_Rule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Email",
                table: "Students");
        }
    }
}
