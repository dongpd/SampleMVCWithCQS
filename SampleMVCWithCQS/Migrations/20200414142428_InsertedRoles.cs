using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleMVCWithCQS.Migrations
{
    public partial class InsertedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5531a806-4235-4b42-b2e1-2a762aab8c37", "a533d286-55d1-48bd-a18c-7625d66f38fc", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "045c5141-0c7c-43eb-a88c-47ada32707f3", "4fbb9a6f-0fa4-45b9-8e6a-258fb0a00874", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "045c5141-0c7c-43eb-a88c-47ada32707f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5531a806-4235-4b42-b2e1-2a762aab8c37");
        }
    }
}
