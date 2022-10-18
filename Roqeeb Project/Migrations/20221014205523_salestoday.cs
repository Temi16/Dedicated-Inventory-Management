using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roqeeb_Project.Migrations
{
    public partial class salestoday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "a8a858eb-63ae-41f8-b166-77d76417a871");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "3cf030a3-7d55-4a90-85fa-ae67c0d947ac");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1cf9d548-4505-4050-b9f7-93029b5014ea");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "d5018885-d210-4ce3-9b47-187039f6f3cf");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "SalesCarts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Week",
                table: "SalesCarts",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { "ee5348b5-4cbd-4857-a9b4-370f458bfead", null, null, null, null, null, false, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "IsEmailConfirmed", "LastModifiedBy", "LastModifiedOn", "LastName", "Password", "Salt", "Username" },
                values: new object[] { "4e7cd7e6-2e6c-45b3-8386-a855e5086e8a", null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, true, null, null, "Temidayo", "temi123idbm1OQckwHMBw==", "idbm1OQckwHMBw==", "RRT" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Age", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName", "UserId" },
                values: new object[] { "69a69f0f-89ec-4290-9e4d-79a16908bf21", 20, null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, null, null, "Temidayo", "4e7cd7e6-2e6c-45b3-8386-a855e5086e8a" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleId", "UserId" },
                values: new object[] { "a6873fff-18f0-4e8f-870f-dbe9ac638f49", null, null, null, null, false, null, null, "ee5348b5-4cbd-4857-a9b4-370f458bfead", "4e7cd7e6-2e6c-45b3-8386-a855e5086e8a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "69a69f0f-89ec-4290-9e4d-79a16908bf21");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "a6873fff-18f0-4e8f-870f-dbe9ac638f49");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ee5348b5-4cbd-4857-a9b4-370f458bfead");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4e7cd7e6-2e6c-45b3-8386-a855e5086e8a");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "SalesCarts");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "SalesCarts");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { "1cf9d548-4505-4050-b9f7-93029b5014ea", null, null, null, null, null, false, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "IsEmailConfirmed", "LastModifiedBy", "LastModifiedOn", "LastName", "Password", "Salt", "Username" },
                values: new object[] { "d5018885-d210-4ce3-9b47-187039f6f3cf", null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, true, null, null, "Temidayo", "temi123hNieRSCFTjdIXw==", "hNieRSCFTjdIXw==", "RRT" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Age", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName", "UserId" },
                values: new object[] { "a8a858eb-63ae-41f8-b166-77d76417a871", 20, null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, null, null, "Temidayo", "d5018885-d210-4ce3-9b47-187039f6f3cf" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleId", "UserId" },
                values: new object[] { "3cf030a3-7d55-4a90-85fa-ae67c0d947ac", null, null, null, null, false, null, null, "1cf9d548-4505-4050-b9f7-93029b5014ea", "d5018885-d210-4ce3-9b47-187039f6f3cf" });
        }
    }
}
