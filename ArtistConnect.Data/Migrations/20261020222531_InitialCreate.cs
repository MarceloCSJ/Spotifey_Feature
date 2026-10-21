using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistConnect.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE TABLE IF NOT EXISTS `Artistas` (
                    `Id` int NOT NULL AUTO_INCREMENT,
                    `Nome` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
                    `Bio` text CHARACTER SET utf8mb4 NULL,
                    `GeneroMusical` varchar(50) CHARACTER SET utf8mb4 NULL,
                    CONSTRAINT `PK_Artistas` PRIMARY KEY (`Id`)
                ) CHARACTER SET=utf8mb4;
                """);
            migrationBuilder.Sql(
                """
                CREATE TABLE IF NOT EXISTS `Musicas` (
                    `Id` int NOT NULL AUTO_INCREMENT,
                    `Titulo` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
                    `ArtistaResponsavel` varchar(100) CHARACTER SET utf8mb4 NULL,
                    `Duracao` varchar(10) CHARACTER SET utf8mb4 NULL,
                    CONSTRAINT `PK_Musicas` PRIMARY KEY (`Id`)
                ) CHARACTER SET=utf8mb4;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // The baseline can include tables that predate EF migrations; do not drop user data.
        }
    }
}
