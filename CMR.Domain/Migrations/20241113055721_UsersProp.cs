using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UsersProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifyOnDate",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyOnDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
