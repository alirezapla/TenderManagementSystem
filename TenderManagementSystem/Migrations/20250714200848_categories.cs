using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TenderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "14b9df6e-41cc-47ed-bc07-91515e20a8ab");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "41257429-a234-4520-98f3-158972a0b384");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "9b9bbf74-e2c7-4e3a-aee3-ae17843a612f");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "9c05054e-0d26-4d45-b839-f10d3be36a3c");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "c308221d-e7e1-4311-af18-fa96b4ff7021");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "1c2b5d67-f332-4b4b-a806-8b6e329fced7", "system", new DateTime(2025, 7, 14, 20, 8, 48, 145, DateTimeKind.Utc).AddTicks(3980), "", null, false, "Healthcare", "", null },
                    { "338b72c6-4141-4c4f-ba31-4dbfdd6efe69", "system", new DateTime(2025, 7, 14, 20, 8, 48, 145, DateTimeKind.Utc).AddTicks(3940), "", null, false, "Construction", "", null },
                    { "6d7e48e5-78b3-458d-be56-f5fbf38fdc68", "system", new DateTime(2025, 7, 14, 20, 8, 48, 145, DateTimeKind.Utc).AddTicks(3961), "", null, false, "IT Services", "", null },
                    { "b4044062-529f-4fce-91e5-3954f2322310", "system", new DateTime(2025, 7, 14, 20, 8, 48, 145, DateTimeKind.Utc).AddTicks(3998), "", null, false, "Education", "", null },
                    { "b602b39d-edb0-48e1-a998-f7d0c75f3641", "system", new DateTime(2025, 7, 14, 20, 8, 48, 145, DateTimeKind.Utc).AddTicks(4031), "", null, false, "Transportation", "", null }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "12bd2cd6-63e8-43b6-ab7a-fb59cd800b14", "system", new DateTime(2025, 7, 14, 20, 8, 48, 155, DateTimeKind.Utc).AddTicks(5100), "", null, false, "open", "", null },
                    { "3823eede-fb7e-42c1-afda-1c1038aab5b4", "system", new DateTime(2025, 7, 14, 20, 8, 48, 155, DateTimeKind.Utc).AddTicks(5156), "", null, false, "approved", "", null },
                    { "993396ba-3576-498a-8802-5bcef5ec81b6", "system", new DateTime(2025, 7, 14, 20, 8, 48, 155, DateTimeKind.Utc).AddTicks(5120), "", null, false, "close ", "", null },
                    { "adec15d9-5b5c-4e11-85c0-850ec4b7e80f", "system", new DateTime(2025, 7, 14, 20, 8, 48, 155, DateTimeKind.Utc).AddTicks(5173), "", null, false, "rejected", "", null },
                    { "bb2b4e12-1742-4137-b172-582b93a71e15", "system", new DateTime(2025, 7, 14, 20, 8, 48, 155, DateTimeKind.Utc).AddTicks(5138), "", null, false, "pending", "", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "1c2b5d67-f332-4b4b-a806-8b6e329fced7");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "338b72c6-4141-4c4f-ba31-4dbfdd6efe69");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "6d7e48e5-78b3-458d-be56-f5fbf38fdc68");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b4044062-529f-4fce-91e5-3954f2322310");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b602b39d-edb0-48e1-a998-f7d0c75f3641");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "12bd2cd6-63e8-43b6-ab7a-fb59cd800b14");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "3823eede-fb7e-42c1-afda-1c1038aab5b4");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "993396ba-3576-498a-8802-5bcef5ec81b6");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "adec15d9-5b5c-4e11-85c0-850ec4b7e80f");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "bb2b4e12-1742-4137-b172-582b93a71e15");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "14b9df6e-41cc-47ed-bc07-91515e20a8ab", "system", new DateTime(2025, 7, 14, 19, 40, 42, 227, DateTimeKind.Utc).AddTicks(748), "", null, false, "open", "", null },
                    { "41257429-a234-4520-98f3-158972a0b384", "system", new DateTime(2025, 7, 14, 19, 40, 42, 227, DateTimeKind.Utc).AddTicks(775), "", null, false, "pending", "", null },
                    { "9b9bbf74-e2c7-4e3a-aee3-ae17843a612f", "system", new DateTime(2025, 7, 14, 19, 40, 42, 227, DateTimeKind.Utc).AddTicks(787), "", null, false, "approved", "", null },
                    { "9c05054e-0d26-4d45-b839-f10d3be36a3c", "system", new DateTime(2025, 7, 14, 19, 40, 42, 227, DateTimeKind.Utc).AddTicks(799), "", null, false, "rejected", "", null },
                    { "c308221d-e7e1-4311-af18-fa96b4ff7021", "system", new DateTime(2025, 7, 14, 19, 40, 42, 227, DateTimeKind.Utc).AddTicks(762), "", null, false, "close ", "", null }
                });
        }
    }
}
