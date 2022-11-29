using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegService.Migrations
{
    public partial class AddedNewmodelUserRegStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileFilled",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserRegStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegStatusId = table.Column<int>(type: "int", nullable: false),
                    UserRegModelId = table.Column<int>(type: "int", nullable: true),
                    FileFilled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRegStatuses_Users_UserRegModelId",
                        column: x => x.UserRegModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRegStatuses_UserRegModelId",
                table: "UserRegStatuses",
                column: "UserRegModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRegStatuses");

            migrationBuilder.AddColumn<bool>(
                name: "FileFilled",
                table: "Users",
                type: "bit",
                nullable: true);
        }
    }
}
