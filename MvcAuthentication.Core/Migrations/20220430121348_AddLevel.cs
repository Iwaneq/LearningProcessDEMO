using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcAuthentication.Core.Migrations
{
    public partial class AddLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelQuestions_LevelProgressStates_LevelId",
                table: "LevelQuestions");

            migrationBuilder.DropColumn(
                name: "LevelName",
                table: "LevelProgressStates");

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "LevelProgressStates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelProgressStates_LevelId",
                table: "LevelProgressStates",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelProgressStates_Levels_LevelId",
                table: "LevelProgressStates",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LevelQuestions_Levels_LevelId",
                table: "LevelQuestions",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelProgressStates_Levels_LevelId",
                table: "LevelProgressStates");

            migrationBuilder.DropForeignKey(
                name: "FK_LevelQuestions_Levels_LevelId",
                table: "LevelQuestions");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_LevelProgressStates_LevelId",
                table: "LevelProgressStates");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "LevelProgressStates");

            migrationBuilder.AddColumn<string>(
                name: "LevelName",
                table: "LevelProgressStates",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelQuestions_LevelProgressStates_LevelId",
                table: "LevelQuestions",
                column: "LevelId",
                principalTable: "LevelProgressStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
