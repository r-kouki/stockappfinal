using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    MatFiscal = table.Column<string>(type: "TEXT", nullable: false),
                    Adresse = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<string>(type: "TEXT", nullable: false),
                    Credit = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    MatFiscal = table.Column<string>(type: "TEXT", nullable: false),
                    Adresse = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<string>(type: "TEXT", nullable: false),
                    Credit = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pieces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Marque = table.Column<string>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    PrixAchatHT = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrixVenteHT = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    SeuilAlerte = table.Column<int>(type: "INTEGER", nullable: false),
                    TvaPct = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacturesVente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturesVente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturesVente_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacturesAchat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FournisseurId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturesAchat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturesAchat_Fournisseurs_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LignesFacture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FactureId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PieceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", nullable: false),
                    RemisePct = table.Column<decimal>(type: "TEXT", nullable: false),
                    FactureAchatId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FactureVenteId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignesFacture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LignesFacture_FacturesAchat_FactureAchatId",
                        column: x => x.FactureAchatId,
                        principalTable: "FacturesAchat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LignesFacture_FacturesVente_FactureVenteId",
                        column: x => x.FactureVenteId,
                        principalTable: "FacturesVente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LignesFacture_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MouvementsStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    PieceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    FactureId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouvementsStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MouvementsStock_Pieces_PieceId",
                        column: x => x.PieceId,
                        principalTable: "Pieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_FacturesAchat_FournisseurId",
                table: "FacturesAchat",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturesVente_ClientId",
                table: "FacturesVente",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_FactureAchatId",
                table: "LignesFacture",
                column: "FactureAchatId");

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_FactureVenteId",
                table: "LignesFacture",
                column: "FactureVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_PieceId",
                table: "LignesFacture",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_MouvementsStock_PieceId",
                table: "MouvementsStock",
                column: "PieceId");

            // Seed initial admin user
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username", "Password", "PasswordHash", "Prenom", "Nom", "Role", "Actif" },
                values: new object[] { Guid.NewGuid(), "admin", "admin", "admin", "admin", "admin", 0, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LignesFacture");

            migrationBuilder.DropTable(
                name: "MouvementsStock");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FacturesAchat");

            migrationBuilder.DropTable(
                name: "FacturesVente");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
} 