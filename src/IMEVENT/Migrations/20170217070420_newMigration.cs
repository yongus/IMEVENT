using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IMEVENT.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IdEvent",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTable",
                table: "Refectories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Events",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "NumberOfTable",
                table: "Refectories");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "IdEvent",
                table: "Events",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "IdEvent");
        }
    }
}
