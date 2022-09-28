using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roqeeb_Project.Migrations
{
    public partial class UpdatePurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "79f50ef4-e0a5-4e50-9c4f-9cab3cae967f");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "dfbf5752-ccdb-4a23-aad1-7d0e7e061484");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7cde3eb4-8be1-4431-8450-d10319571bf5");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "398d04b2-4244-4fe0-b650-5909c2d04acd");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Purchases",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SupplierId",
                table: "Purchases",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    SupplierName = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { "e28fb951-9daa-4fdf-b234-7ca5510360a4", null, null, null, null, null, false, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "IsEmailConfirmed", "LastModifiedBy", "LastModifiedOn", "LastName", "Password", "Salt", "Username" },
                values: new object[] { "9a09165c-2433-43b9-8801-2842e8db9090", null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, true, null, null, "Temidayo", "temi123sbVuq4FELk7aoA==", "sbVuq4FELk7aoA==", "RRT" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Age", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName", "UserId" },
                values: new object[] { "a4fd3f61-f2e2-4d62-87da-e7253e8de75e", 20, null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, null, null, "Temidayo", "9a09165c-2433-43b9-8801-2842e8db9090" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleId", "UserId" },
                values: new object[] { "c79bfb62-9703-45ee-8fb2-4d870640089e", null, null, null, null, false, null, null, "e28fb951-9daa-4fdf-b234-7ca5510360a4", "9a09165c-2433-43b9-8801-2842e8db9090" });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SupplierId",
                table: "Purchases",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                table: "Purchases",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Suppliers_SupplierId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SupplierId",
                table: "Purchases");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "a4fd3f61-f2e2-4d62-87da-e7253e8de75e");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "c79bfb62-9703-45ee-8fb2-4d870640089e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e28fb951-9daa-4fdf-b234-7ca5510360a4");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "9a09165c-2433-43b9-8801-2842e8db9090");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Purchases");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { "7cde3eb4-8be1-4431-8450-d10319571bf5", null, null, null, null, null, false, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "IsEmailConfirmed", "LastModifiedBy", "LastModifiedOn", "LastName", "Password", "Salt", "Username" },
                values: new object[] { "398d04b2-4244-4fe0-b650-5909c2d04acd", null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, true, null, null, "Temidayo", "temi123zrLKNa4QBZuOuQ==", "zrLKNa4QBZuOuQ==", "RRT" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Age", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName", "UserId" },
                values: new object[] { "79f50ef4-e0a5-4e50-9c4f-9cab3cae967f", 20, null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, null, null, "Temidayo", "398d04b2-4244-4fe0-b650-5909c2d04acd" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleId", "UserId" },
                values: new object[] { "dfbf5752-ccdb-4a23-aad1-7d0e7e061484", null, null, null, null, false, null, null, "7cde3eb4-8be1-4431-8450-d10319571bf5", "398d04b2-4244-4fe0-b650-5909c2d04acd" });
        }
    }
}
