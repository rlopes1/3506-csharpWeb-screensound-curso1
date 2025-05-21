using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarArtistaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 1 WHERE Id = 1");

            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 1 WHERE Id = 2");

            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 1 WHERE Id = 3");

            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 1 WHERE Id = 4");

            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 2 WHERE Id = 5");

            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 2 WHERE Id = 6");

            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 2 WHERE Id = 7");

            //migrationBuilder.Sql("UPDATE Artistas SET Id = 5 WHERE Id = 7");




        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
