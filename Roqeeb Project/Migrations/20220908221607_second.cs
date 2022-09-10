using Microsoft.EntityFrameworkCore.Migrations;

namespace Roqeeb_Project.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sections_SectionId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSales_Products_ProductId1",
                table: "ProductsSales");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSales_Sales_SalesId1",
                table: "ProductsSales");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSales_ProductId1",
                table: "ProductsSales");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSales_SalesId1",
                table: "ProductsSales");

            migrationBuilder.DropIndex(
                name: "IX_Products_SectionId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "82d1c75c-15c0-4025-a3d8-2619c029df16");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "5df1e99b-84fe-4d80-9478-06ffd20b0136");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "22e0165f-c9e7-4d0d-8a54-0bc69c94ab5f");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b1c516f7-40a6-4710-824d-7b1cd55f2f73");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductsSales");

            migrationBuilder.DropColumn(
                name: "SalesId1",
                table: "ProductsSales");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "SalesId",
                table: "ProductsSales",
                type: "varchar(767)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductsSales",
                type: "varchar(767)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { "a113de61-6504-400a-a8ec-8fff84e9e88c", null, null, null, null, null, false, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "IsEmailConfirmed", "LastModifiedBy", "LastModifiedOn", "LastName", "Password", "Username" },
                values: new object[] { "3d531df7-0067-4553-b0e0-6de14ec0bbe6", null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, false, null, null, "Temidayo", "temi123", "RRT" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Age", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName", "UserId" },
                values: new object[] { "a56878ea-51b1-48c5-8571-2287eb112004", 20, null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, null, null, "Temidayo", "3d531df7-0067-4553-b0e0-6de14ec0bbe6" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleId", "UserId" },
                values: new object[] { "155c2d05-13c3-42f9-b3e1-b19b126e662e", null, null, null, null, false, null, null, "a113de61-6504-400a-a8ec-8fff84e9e88c", "3d531df7-0067-4553-b0e0-6de14ec0bbe6" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSales_ProductId",
                table: "ProductsSales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSales_SalesId",
                table: "ProductsSales",
                column: "SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSales_Products_ProductId",
                table: "ProductsSales",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSales_Sales_SalesId",
                table: "ProductsSales",
                column: "SalesId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSales_Products_ProductId",
                table: "ProductsSales");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSales_Sales_SalesId",
                table: "ProductsSales");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSales_ProductId",
                table: "ProductsSales");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSales_SalesId",
                table: "ProductsSales");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "a56878ea-51b1-48c5-8571-2287eb112004");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "155c2d05-13c3-42f9-b3e1-b19b126e662e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a113de61-6504-400a-a8ec-8fff84e9e88c");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3d531df7-0067-4553-b0e0-6de14ec0bbe6");

            migrationBuilder.AlterColumn<int>(
                name: "SalesId",
                table: "ProductsSales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(767)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductsSales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(767)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "ProductsSales",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesId1",
                table: "ProductsSales",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectionId",
                table: "Products",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { "22e0165f-c9e7-4d0d-8a54-0bc69c94ab5f", null, null, null, null, null, false, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "IsEmailConfirmed", "LastModifiedBy", "LastModifiedOn", "LastName", "Password", "Username" },
                values: new object[] { "b1c516f7-40a6-4710-824d-7b1cd55f2f73", null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, false, null, null, "Temidayo", "temi123", "RRT" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Age", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName", "UserId" },
                values: new object[] { "82d1c75c-15c0-4025-a3d8-2619c029df16", 20, null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, null, null, "Temidayo", "b1c516f7-40a6-4710-824d-7b1cd55f2f73" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleId", "UserId" },
                values: new object[] { "5df1e99b-84fe-4d80-9478-06ffd20b0136", null, null, null, null, false, null, null, "22e0165f-c9e7-4d0d-8a54-0bc69c94ab5f", "b1c516f7-40a6-4710-824d-7b1cd55f2f73" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSales_ProductId1",
                table: "ProductsSales",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSales_SalesId1",
                table: "ProductsSales",
                column: "SalesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SectionId",
                table: "Products",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sections_SectionId",
                table: "Products",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSales_Products_ProductId1",
                table: "ProductsSales",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSales_Sales_SalesId1",
                table: "ProductsSales",
                column: "SalesId1",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
