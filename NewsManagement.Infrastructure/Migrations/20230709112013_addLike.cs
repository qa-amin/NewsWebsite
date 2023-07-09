using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsManagement.Infrastructure.EFCore.Migrations
{
    public partial class addLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    NewsId = table.Column<long>(type: "bigint", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.IpAddress, x.NewsId });
                    table.ForeignKey(
                        name: "FK_Likes_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NewsId",
                table: "Likes",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");
        }
    }
}
