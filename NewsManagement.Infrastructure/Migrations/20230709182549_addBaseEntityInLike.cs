using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsManagement.Infrastructure.EFCore.Migrations
{
    public partial class addBaseEntityInLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Likes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemove",
                table: "Likes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveDate",
                table: "Likes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Likes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "IsRemove",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "RemoveDate",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Likes");
        }
    }
}
