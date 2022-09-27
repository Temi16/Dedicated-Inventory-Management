using Microsoft.EntityFrameworkCore.Migrations;

namespace Roqeeb_Project.Migrations
{
    public partial class updatedConext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: "f0367cde-a9d8-4ebf-957f-df55f3405130");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: "0c9ea518-6811-4ed3-b2d5-f0106ec1f9d5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7ac5dde7-fcb7-4ab4-b7ed-ca3435e9970b");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "7cb3e52d-712f-44eb-961e-c3585ad7a35a");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { "7ac5dde7-fcb7-4ab4-b7ed-ca3435e9970b", null, null, null, null, null, false, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "IsEmailConfirmed", "LastModifiedBy", "LastModifiedOn", "LastName", "Password", "Salt", "Username" },
                values: new object[] { "7cb3e52d-712f-44eb-961e-c3585ad7a35a", null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, true, null, null, "Temidayo", "temi123", null, "RRT" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Age", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName", "UserId" },
                values: new object[] { "f0367cde-a9d8-4ebf-957f-df55f3405130", 20, null, null, null, null, "raufroqeeb123@gmail.com", "Roqeeb", false, null, null, "Temidayo", "7cb3e52d-712f-44eb-961e-c3585ad7a35a" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleId", "UserId" },
                values: new object[] { "0c9ea518-6811-4ed3-b2d5-f0106ec1f9d5", null, null, null, null, false, null, null, "7ac5dde7-fcb7-4ab4-b7ed-ca3435e9970b", "7cb3e52d-712f-44eb-961e-c3585ad7a35a" });
        }
    }
}
