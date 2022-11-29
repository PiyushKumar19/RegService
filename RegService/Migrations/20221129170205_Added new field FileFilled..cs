using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegService.Migrations
{
    public partial class AddednewfieldFileFilled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AadhaarNo",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "Branch",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "District",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "IFSCcode",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "PanNo",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "Pincode",
                table: "RegisterViewModel");

            migrationBuilder.DropColumn(
                name: "State",
                table: "RegisterViewModel");

            migrationBuilder.RenameColumn(
                name: "Numeber",
                table: "LoginTestingViewModel",
                newName: "ContactNo");

            migrationBuilder.AddColumn<bool>(
                name: "FileFilled",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileFilled",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ContactNo",
                table: "LoginTestingViewModel",
                newName: "Numeber");

            migrationBuilder.AddColumn<string>(
                name: "AadhaarNo",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IFSCcode",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PanNo",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pincode",
                table: "RegisterViewModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "RegisterViewModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
