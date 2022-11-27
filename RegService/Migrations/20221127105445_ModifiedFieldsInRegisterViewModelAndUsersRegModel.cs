using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegService.Migrations
{
    public partial class ModifiedFieldsInRegisterViewModelAndUsersRegModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNo",
                table: "Users",
                newName: "ContactNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNo",
                table: "Users",
                newName: "PhoneNo");
        }
    }
}
