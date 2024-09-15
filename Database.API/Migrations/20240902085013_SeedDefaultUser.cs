using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: new Guid("3079fc81-42de-40b5-8eb6-8d43da38ec03"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: new Guid("df4e5fa0-4e5c-4e92-9f68-1e410f3a7318"));

            migrationBuilder.DeleteData(
                table: "ItemLocations",
                keyColumn: "LocationId",
                keyValue: new Guid("92a8cbd2-bcaa-4163-a83c-e756fcb71dc2"));

            migrationBuilder.DeleteData(
                table: "ItemLocations",
                keyColumn: "LocationId",
                keyValue: new Guid("aafc3e7c-e83a-41ef-889f-b15493841526"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1bfd041b-a3c1-4e2c-9e07-20080117e5bb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("669b2257-c57a-4297-b350-02aa10597e94"));

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1573d5e5-f41f-4f25-926b-2009d7ddd302"), null, "User", "USER" },
                    { new Guid("4d206423-bd5c-4819-a8f4-3b6bb5c26e28"), null, "Adminstrator", "ADMINSTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUserRole<string>",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1573D5E5-F41F-4F25-926B-2009D7DDD302", "33c779de-3ca8-4ee6-9450-955614e289f8" },
                    { "4D206423-BD5C-4819-A8F4-3B6BB5C26E28", "52f82a3c-aa91-44be-b847-ff98503879fa" }
                });

            migrationBuilder.InsertData(
                table: "ItemLocations",
                columns: new[] { "LocationId", "LocationDetail", "LocationName" },
                values: new object[,]
                {
                    { new Guid("dc091244-972f-4132-8e05-5f2c358c4404"), "Main Warehouse", "Warehouse A" },
                    { new Guid("e7333577-bc78-4254-9f22-13f9078306ee"), "Secondary Warehouse", "Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Department", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonName", "PhoneNumber", "PhoneNumberConfirmed", "RoleLevel", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("33c779de-3ca8-4ee6-9450-955614e289f8"), 0, "0927ee5e-0819-42e6-b330-0513c1a61398", "Sales", "john.doe@example.com", false, false, null, "JOHN.DOE@EXAMPLE.COM", "JOHN DOE", "AQAAAAIAAYagAAAAEP1gLT9Lo7gB2JeOti36WIH5NWd5+afvIkPXXw3x3sI3Jc5Irjzkgd7JXLALwAYF1A==", "白菜", null, false, 2, null, false, "John Doe" },
                    { new Guid("52f82a3c-aa91-44be-b847-ff98503879fa"), 0, "82826ffa-78aa-41ec-ba1f-88886656cb8c", "Management", "admin@example.com", false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEJ8Cx9vFTMwCdTowkeFvxRbM6U3W4pcvzQIz/zxa3oApjOtPdfGCnF7tLv8956D0Yw==", "芳儀", null, false, 1, null, false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "CreateUserId", "Description", "ItemLocationId", "ItemName", "LocationName", "PurchaseId", "Status" },
                values: new object[,]
                {
                    { new Guid("2f485982-7ae4-4c9f-99d4-48dc018bbca3"), new Guid("33c779de-3ca8-4ee6-9450-955614e289f8"), "Item 2 Description", new Guid("e7333577-bc78-4254-9f22-13f9078306ee"), "Item 2", "Warehouse B", 1002, "正常" },
                    { new Guid("a7aa7b0d-13b5-49a6-8dcd-902e0343ee2f"), new Guid("52f82a3c-aa91-44be-b847-ff98503879fa"), "Item 1 Description", new Guid("dc091244-972f-4132-8e05-5f2c358c4404"), "Item 1", "Warehouse A", 1001, "正常" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUserRole<string>");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1573d5e5-f41f-4f25-926b-2009d7ddd302"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4d206423-bd5c-4819-a8f4-3b6bb5c26e28"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: new Guid("2f485982-7ae4-4c9f-99d4-48dc018bbca3"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: new Guid("a7aa7b0d-13b5-49a6-8dcd-902e0343ee2f"));

            migrationBuilder.DeleteData(
                table: "ItemLocations",
                keyColumn: "LocationId",
                keyValue: new Guid("dc091244-972f-4132-8e05-5f2c358c4404"));

            migrationBuilder.DeleteData(
                table: "ItemLocations",
                keyColumn: "LocationId",
                keyValue: new Guid("e7333577-bc78-4254-9f22-13f9078306ee"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33c779de-3ca8-4ee6-9450-955614e289f8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("52f82a3c-aa91-44be-b847-ff98503879fa"));

            migrationBuilder.InsertData(
                table: "ItemLocations",
                columns: new[] { "LocationId", "LocationDetail", "LocationName" },
                values: new object[,]
                {
                    { new Guid("92a8cbd2-bcaa-4163-a83c-e756fcb71dc2"), "Secondary Warehouse", "Warehouse B" },
                    { new Guid("aafc3e7c-e83a-41ef-889f-b15493841526"), "Main Warehouse", "Warehouse A" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Department", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonName", "PhoneNumber", "PhoneNumberConfirmed", "RoleLevel", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1bfd041b-a3c1-4e2c-9e07-20080117e5bb"), 0, "05883323-67d9-457f-aef5-6b8cf90295c6", "Sales", "john.doe@example.com", false, false, null, "JOHN.DOE@EXAMPLE.COM", "JOHN DOE", "AQAAAAIAAYagAAAAEPn1eio15V/KPH/UYy9a9EpEbSAUdSB5GF8Kcb0wZYwxcQD2bzL67xjnKQbT+X2hZA==", "白菜", null, false, 2, null, false, "John Doe" },
                    { new Guid("669b2257-c57a-4297-b350-02aa10597e94"), 0, "3ff8fa18-336e-462a-999b-38a9fb5df94d", "Management", "admin@example.com", false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAENDrOp3h5Wj6OL3qFGBGT9b5nSPSFsAM96BzkOiowDHLbY3eGDW4m/qDfQE9wKkpng==", "芳儀", null, false, 1, null, false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "CreateUserId", "Description", "ItemLocationId", "ItemName", "LocationName", "PurchaseId", "Status" },
                values: new object[,]
                {
                    { new Guid("3079fc81-42de-40b5-8eb6-8d43da38ec03"), new Guid("1bfd041b-a3c1-4e2c-9e07-20080117e5bb"), "Item 2 Description", new Guid("92a8cbd2-bcaa-4163-a83c-e756fcb71dc2"), "Item 2", "Warehouse B", 1002, "正常" },
                    { new Guid("df4e5fa0-4e5c-4e92-9f68-1e410f3a7318"), new Guid("669b2257-c57a-4297-b350-02aa10597e94"), "Item 1 Description", new Guid("aafc3e7c-e83a-41ef-889f-b15493841526"), "Item 1", "Warehouse A", 1001, "正常" }
                });
        }
    }
}
