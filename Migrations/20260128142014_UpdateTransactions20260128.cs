using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFinance.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactions20260128 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Categories_FamilyId",
                table: "Categories",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Families_FamilyId",
                table: "Categories",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Families_FamilyId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_FamilyId",
                table: "Categories");
        }
    }
}
