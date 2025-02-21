using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class WatchList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "WatchLists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WatchLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

    
            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_UserId_MovieId",
                table: "WatchLists",
                columns: new[] { "UserId", "MovieId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLists_AspNetUsers_UserId",
                table: "WatchLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLists_Movies_MovieId1",
                table: "WatchLists",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchLists_AspNetUsers_UserId",
                table: "WatchLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLists_Movies_MovieId1",
                table: "WatchLists");

            migrationBuilder.DropIndex(
                name: "IX_WatchLists_MovieId1",
                table: "WatchLists");

            migrationBuilder.DropIndex(
                name: "IX_WatchLists_UserId_MovieId",
                table: "WatchLists");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "WatchLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WatchLists");
        }
    }
}
