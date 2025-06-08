using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skypoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFollow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_Users_FollowedId",
                table: "UserFollows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFollows",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FollowedId",
                table: "UserFollows",
                newName: "FolloweeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_FollowedId",
                table: "UserFollows",
                newName: "IX_UserFollows_FolloweeId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserFollows",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "FollowedAt",
                table: "UserFollows",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFollows",
                table: "UserFollows",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollows_FollowerId_FolloweeId",
                table: "UserFollows",
                columns: new[] { "FollowerId", "FolloweeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_Users_FolloweeId",
                table: "UserFollows",
                column: "FolloweeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_Users_FolloweeId",
                table: "UserFollows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFollows",
                table: "UserFollows");

            migrationBuilder.DropIndex(
                name: "IX_UserFollows_FollowerId_FolloweeId",
                table: "UserFollows");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFollows");

            migrationBuilder.DropColumn(
                name: "FollowedAt",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FolloweeId",
                table: "UserFollows",
                newName: "FollowedId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_FolloweeId",
                table: "UserFollows",
                newName: "IX_UserFollows_FollowedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFollows",
                table: "UserFollows",
                columns: new[] { "FollowerId", "FollowedId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_Users_FollowedId",
                table: "UserFollows",
                column: "FollowedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
