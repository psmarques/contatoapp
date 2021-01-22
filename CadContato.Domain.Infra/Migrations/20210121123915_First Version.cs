using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadContato.Domain.Infra.Migrations
{
    public partial class FirstVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeCompleto = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    EmailUser = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true, defaultValue: ""),
                    UltimoNome = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true, defaultValue: ""),
                    Email = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true, defaultValue: ""),
                    TelefoneDDD = table.Column<int>(type: "INTEGER", nullable: true),
                    TelefoneNumero = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_UserId",
                table: "Contato",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
