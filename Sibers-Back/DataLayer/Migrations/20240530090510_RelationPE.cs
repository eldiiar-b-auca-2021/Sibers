using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RelationPE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Employees_EmpId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_ProjectEmployee_PrId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployee_Employees_project_manager_id",
                table: "ProjectEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEmployee",
                table: "ProjectEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects");

            migrationBuilder.RenameTable(
                name: "ProjectEmployee",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "EmployeeProjects",
                newName: "ProjectEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEmployee_project_manager_id",
                table: "Projects",
                newName: "IX_Projects_project_manager_id");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProjects_PrId",
                table: "ProjectEmployees",
                newName: "IX_ProjectEmployees_PrId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees",
                columns: new[] { "EmpId", "PrId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_Employees_EmpId",
                table: "ProjectEmployees",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployees_Projects_PrId",
                table: "ProjectEmployees",
                column: "PrId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_project_manager_id",
                table: "Projects",
                column: "project_manager_id",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_Employees_EmpId",
                table: "ProjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployees_Projects_PrId",
                table: "ProjectEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_project_manager_id",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEmployees",
                table: "ProjectEmployees");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "ProjectEmployee");

            migrationBuilder.RenameTable(
                name: "ProjectEmployees",
                newName: "EmployeeProjects");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_project_manager_id",
                table: "ProjectEmployee",
                newName: "IX_ProjectEmployee_project_manager_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEmployees_PrId",
                table: "EmployeeProjects",
                newName: "IX_EmployeeProjects_PrId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEmployee",
                table: "ProjectEmployee",
                column: "ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects",
                columns: new[] { "EmpId", "PrId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Employees_EmpId",
                table: "EmployeeProjects",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_ProjectEmployee_PrId",
                table: "EmployeeProjects",
                column: "PrId",
                principalTable: "ProjectEmployee",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEmployee_Employees_project_manager_id",
                table: "ProjectEmployee",
                column: "project_manager_id",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
