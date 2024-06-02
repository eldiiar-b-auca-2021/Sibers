using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Enums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Projects_PrId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_project_manager_id",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "ProjectEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_project_manager_id",
                table: "ProjectEmployee",
                newName: "IX_ProjectEmployee_project_manager_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectEmployee",
                table: "ProjectEmployee",
                column: "ProjectId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_ProjectEmployee_PrId",
                table: "EmployeeProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEmployee_Employees_project_manager_id",
                table: "ProjectEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectEmployee",
                table: "ProjectEmployee");

            migrationBuilder.RenameTable(
                name: "ProjectEmployee",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEmployee_project_manager_id",
                table: "Projects",
                newName: "IX_Projects_project_manager_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Projects_PrId",
                table: "EmployeeProjects",
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
    }
}
