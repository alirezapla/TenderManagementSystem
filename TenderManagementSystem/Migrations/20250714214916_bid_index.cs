using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TenderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class bid_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "1c2b5d67-f332-4b4b-a806-8b6e329fced7",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 404, DateTimeKind.Utc).AddTicks(6759));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "338b72c6-4141-4c4f-ba31-4dbfdd6efe69",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 404, DateTimeKind.Utc).AddTicks(6772));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "6d7e48e5-78b3-458d-be56-f5fbf38fdc68",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 404, DateTimeKind.Utc).AddTicks(6796));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b4044062-529f-4fce-91e5-3954f2322310",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 404, DateTimeKind.Utc).AddTicks(6806));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b602b39d-edb0-48e1-a998-f7d0c75f3641",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 404, DateTimeKind.Utc).AddTicks(6816));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "12bd2cd6-63e8-43b6-ab7a-fb59cd800b14",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 419, DateTimeKind.Utc).AddTicks(5332));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "3823eede-fb7e-42c1-afda-1c1038aab5b4",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 419, DateTimeKind.Utc).AddTicks(5345));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "993396ba-3576-498a-8802-5bcef5ec81b6",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 419, DateTimeKind.Utc).AddTicks(5355));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "adec15d9-5b5c-4e11-85c0-850ec4b7e80f",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 419, DateTimeKind.Utc).AddTicks(5364));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "bb2b4e12-1742-4137-b172-582b93a71e15",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 49, 16, 419, DateTimeKind.Utc).AddTicks(5374));
            

            migrationBuilder.CreateIndex(
                name: "IX_Bids_VendorId_TenderId",
                table: "Bids",
                columns: new[] { "VendorId", "TenderId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_Bids_VendorId_TenderId",
                table: "Bids");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "1c2b5d67-f332-4b4b-a806-8b6e329fced7",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 742, DateTimeKind.Utc).AddTicks(9193));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "338b72c6-4141-4c4f-ba31-4dbfdd6efe69",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 742, DateTimeKind.Utc).AddTicks(9210));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "6d7e48e5-78b3-458d-be56-f5fbf38fdc68",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 742, DateTimeKind.Utc).AddTicks(9222));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b4044062-529f-4fce-91e5-3954f2322310",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 742, DateTimeKind.Utc).AddTicks(9233));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b602b39d-edb0-48e1-a998-f7d0c75f3641",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 742, DateTimeKind.Utc).AddTicks(9242));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "12bd2cd6-63e8-43b6-ab7a-fb59cd800b14",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 752, DateTimeKind.Utc).AddTicks(2692));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "3823eede-fb7e-42c1-afda-1c1038aab5b4",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 752, DateTimeKind.Utc).AddTicks(2704));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "993396ba-3576-498a-8802-5bcef5ec81b6",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 752, DateTimeKind.Utc).AddTicks(2713));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "adec15d9-5b5c-4e11-85c0-850ec4b7e80f",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 752, DateTimeKind.Utc).AddTicks(2722));

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: "bb2b4e12-1742-4137-b172-582b93a71e15",
                column: "CreatedDate",
                value: new DateTime(2025, 7, 14, 21, 48, 13, 752, DateTimeKind.Utc).AddTicks(2730));
            
        }
    }
}
