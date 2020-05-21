﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMS.CoderePlaytech.Infrastructure.Migrations
{
    public partial class First_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarcodeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarcodeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Barcodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BarcodeTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barcodes_BarcodeTypes_BarcodeTypeId",
                        column: x => x.BarcodeTypeId,
                        principalTable: "BarcodeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Deposit" });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Withdrawal" });

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_BarcodeTypeId",
                table: "Barcodes",
                column: "BarcodeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barcodes");

            migrationBuilder.DropTable(
                name: "BarcodeTypes");
        }
    }
}
