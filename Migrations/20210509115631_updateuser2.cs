using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDev.Migrations
{
    public partial class updateuser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Posts",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Posts",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Posts",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Posts",
                newName: "CreatedOn");
        }
    }
}
