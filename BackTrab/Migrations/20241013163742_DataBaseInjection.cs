using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseInjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                    name: "Email",
                    table: "tbUser",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "varchar(255)",
                    oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                    name: "Email",
                    table: "tbUser",
                    type: "varchar(255)",
                    maxLength: 255,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}