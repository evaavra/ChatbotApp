using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatbotMyAxelos.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PDFFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDFFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeperatedTexts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 1050, nullable: false),
                    PDFFileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeperatedTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeperatedTexts_PDFFiles_PDFFileId",
                        column: x => x.PDFFileId,
                        principalTable: "PDFFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeperatedTexts_PDFFileId",
                table: "SeperatedTexts",
                column: "PDFFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeperatedTexts");

            migrationBuilder.DropTable(
                name: "PDFFiles");
        }
    }
}
