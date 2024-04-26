using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.Migrations
{
    /// <inheritdoc />
    public partial class AddViaEventMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    _FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    _LastName = table.Column<string>(type: "TEXT", nullable: false),
                    _Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViaEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    _EventTitle = table.Column<string>(type: "TEXT", nullable: true),
                    _Description = table.Column<string>(type: "TEXT", nullable: true),
                    _StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    _EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    _MaxNumberOfGuests = table.Column<int>(type: "INTEGER", nullable: true),
                    _EventVisibility = table.Column<string>(type: "TEXT", nullable: true),
                    _EventStatus = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViaEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestParticipation",
                columns: table => new
                {
                    GuestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestParticipation", x => new { x.EventId, x.GuestId });
                    table.ForeignKey(
                        name: "FK_GuestParticipation_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestParticipation_ViaEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "ViaEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    _InvitationStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    _EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    _GuestId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_ViaEvents__EventId",
                        column: x => x._EventId,
                        principalTable: "ViaEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestParticipation_GuestId",
                table: "GuestParticipation",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations__EventId",
                table: "Invitations",
                column: "_EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestParticipation");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "ViaEvents");
        }
    }
}
