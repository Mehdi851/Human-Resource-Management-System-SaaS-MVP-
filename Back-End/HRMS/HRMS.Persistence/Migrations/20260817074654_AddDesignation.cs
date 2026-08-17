using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDesignation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designation_Departments_DepartmentId",
                table: "Designation");

            migrationBuilder.DropForeignKey(
                name: "FK_Designation_Organizations_OrganizationId",
                table: "Designation");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Designation_DesignationId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Designation",
                table: "Designation");

            migrationBuilder.RenameTable(
                name: "Designation",
                newName: "Designations");

            migrationBuilder.RenameIndex(
                name: "IX_Designation_OrganizationId",
                table: "Designations",
                newName: "IX_Designations_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Designation_DepartmentId",
                table: "Designations",
                newName: "IX_Designations_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Designations",
                table: "Designations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_Departments_DepartmentId",
                table: "Designations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_Organizations_OrganizationId",
                table: "Designations",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Designations_DesignationId",
                table: "Employees",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designations_Departments_DepartmentId",
                table: "Designations");

            migrationBuilder.DropForeignKey(
                name: "FK_Designations_Organizations_OrganizationId",
                table: "Designations");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Designations_DesignationId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Designations",
                table: "Designations");

            migrationBuilder.RenameTable(
                name: "Designations",
                newName: "Designation");

            migrationBuilder.RenameIndex(
                name: "IX_Designations_OrganizationId",
                table: "Designation",
                newName: "IX_Designation_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Designations_DepartmentId",
                table: "Designation",
                newName: "IX_Designation_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Designation",
                table: "Designation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Designation_Departments_DepartmentId",
                table: "Designation",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Designation_Organizations_OrganizationId",
                table: "Designation",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Designation_DesignationId",
                table: "Employees",
                column: "DesignationId",
                principalTable: "Designation",
                principalColumn: "Id");
        }
    }
}
