﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Songify.Simple.Migrations
{
    public partial class InitialMigration : Migration
    {
        // Sposoby na migracje
        // - migracja na starcie aplikacji  np w db context
        //   Database.Migrate( ....)
        //- konsolowo dotnet ef update -v
        // Po utworzeniu migracji
        // Skoro nie zostala zadelarowana zadna schema, to standardową jest dbo
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });
            
            // Ourt place f.ex.:
            // migrationBuilder.CreateSequence()
            // migrationBuilder.Sql("Create view")
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
