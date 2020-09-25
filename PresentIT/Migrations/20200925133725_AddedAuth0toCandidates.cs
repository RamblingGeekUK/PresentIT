using Microsoft.EntityFrameworkCore.Migrations;

namespace PresentIT.Migrations
{
    public partial class AddedAuth0toCandidates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auth0",
                table: "Candidate",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth0",
                table: "Candidate");
        }
    }
}
