using Microsoft.EntityFrameworkCore.Migrations;

namespace rent.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firme",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firme", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telefon = table.Column<int>(type: "int", maxLength: 11, nullable: false),
                    FirmaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korisnici_Firme_FirmaID",
                        column: x => x.FirmaID,
                        principalTable: "Firme",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vozila",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tablice = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    BrojOcenjivanja = table.Column<int>(type: "int", nullable: false),
                    Ocena = table.Column<double>(type: "float", nullable: false),
                    FirmeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozila", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vozila_Firme_FirmeID",
                        column: x => x.FirmeID,
                        principalTable: "Firme",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Iznajmljivanja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    FirmaID = table.Column<int>(type: "int", nullable: false),
                    VoziloID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iznajmljivanja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Iznajmljivanja_Firme_FirmaID",
                        column: x => x.FirmaID,
                        principalTable: "Firme",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Iznajmljivanja_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Iznajmljivanja_Vozila_VoziloID",
                        column: x => x.VoziloID,
                        principalTable: "Vozila",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanja_FirmaID",
                table: "Iznajmljivanja",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanja_KorisnikID",
                table: "Iznajmljivanja",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanja_VoziloID",
                table: "Iznajmljivanja",
                column: "VoziloID");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_FirmaID",
                table: "Korisnici",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_FirmeID",
                table: "Vozila",
                column: "FirmeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Iznajmljivanja");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Vozila");

            migrationBuilder.DropTable(
                name: "Firme");
        }
    }
}
