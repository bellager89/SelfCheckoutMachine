using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfCheckoutMachine.DataAccess.Migrations
{
    public partial class RemoveEurValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueInEur",
                table: "Currency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValueInEur",
                table: "Currency",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
