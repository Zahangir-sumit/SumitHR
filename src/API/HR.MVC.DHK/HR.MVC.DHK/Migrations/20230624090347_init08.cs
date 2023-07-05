using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.MVC.DHK.Migrations
{
    /// <inheritdoc />
    public partial class init08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hrent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsInactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ComId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DeptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeptName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => new { x.DeptId, x.ComId });
                    table.ForeignKey(
                        name: "FK_Department_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    DesigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesigName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => new { x.DesigId, x.ComId });
                    table.ForeignKey(
                        name: "FK_Designation_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShiftIn = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShiftOut = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShiftLate = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => new { x.ShiftId, x.ComId });
                    table.ForeignKey(
                        name: "FK_Shift_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    dtJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HRent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Others = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => new { x.EmpId, x.ComId });
                    table.ForeignKey(
                        name: "FK_Employee_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DeptId_ComId",
                        columns: x => new { x.DeptId, x.ComId },
                        principalTable: "Department",
                        principalColumns: new[] { "DeptId", "ComId" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employee_Designation_DesigId_ComId",
                        columns: x => new { x.DesigId, x.ComId },
                        principalTable: "Designation",
                        principalColumns: new[] { "DesigId", "ComId" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employee_Shift_ShiftId_ComId",
                        columns: x => new { x.ShiftId, x.ComId },
                        principalTable: "Shift",
                        principalColumns: new[] { "ShiftId", "ComId" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttStatus = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    InTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OutTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CompanyComId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeComId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeEmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Company_CompanyComId",
                        column: x => x.CompanyComId,
                        principalTable: "Company",
                        principalColumn: "ComId");
                    table.ForeignKey(
                        name: "FK_Attendance_Employee_EmpId_ComId",
                        columns: x => new { x.EmpId, x.ComId },
                        principalTable: "Employee",
                        principalColumns: new[] { "EmpId", "ComId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Employee_EmployeeEmpId_EmployeeComId",
                        columns: x => new { x.EmployeeEmpId, x.EmployeeComId },
                        principalTable: "Employee",
                        principalColumns: new[] { "EmpId", "ComId" });
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtYear = table.Column<int>(type: "int", nullable: false),
                    DtMonth = table.Column<int>(type: "int", nullable: false),
                    Present = table.Column<int>(type: "int", nullable: true),
                    Late = table.Column<int>(type: "int", nullable: true),
                    Absent = table.Column<int>(type: "int", nullable: true),
                    CompanyComId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeComId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeEmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceSummary_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceSummary_Company_CompanyComId",
                        column: x => x.CompanyComId,
                        principalTable: "Company",
                        principalColumn: "ComId");
                    table.ForeignKey(
                        name: "FK_AttendanceSummary_Employee_EmpId_ComId",
                        columns: x => new { x.EmpId, x.ComId },
                        principalTable: "Employee",
                        principalColumns: new[] { "EmpId", "ComId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceSummary_Employee_EmployeeEmpId_EmployeeComId",
                        columns: x => new { x.EmployeeEmpId, x.EmployeeComId },
                        principalTable: "Employee",
                        principalColumns: new[] { "EmpId", "ComId" });
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtYear = table.Column<int>(type: "int", nullable: false),
                    DtMonth = table.Column<int>(type: "int", nullable: false),
                    Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hrent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AbsentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyComId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeComId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeEmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salary_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salary_Company_CompanyComId",
                        column: x => x.CompanyComId,
                        principalTable: "Company",
                        principalColumn: "ComId");
                    table.ForeignKey(
                        name: "FK_Salary_Employee_EmpId_ComId",
                        columns: x => new { x.EmpId, x.ComId },
                        principalTable: "Employee",
                        principalColumns: new[] { "EmpId", "ComId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salary_Employee_EmployeeEmpId_EmployeeComId",
                        columns: x => new { x.EmployeeEmpId, x.EmployeeComId },
                        principalTable: "Employee",
                        principalColumns: new[] { "EmpId", "ComId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ComId_EmpId_DtDate",
                table: "Attendance",
                columns: new[] { "ComId", "EmpId", "DtDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_CompanyComId",
                table: "Attendance",
                column: "CompanyComId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmpId_ComId",
                table: "Attendance",
                columns: new[] { "EmpId", "ComId" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeEmpId_EmployeeComId",
                table: "Attendance",
                columns: new[] { "EmployeeEmpId", "EmployeeComId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummary_ComId_EmpId_DtYear_DtMonth",
                table: "AttendanceSummary",
                columns: new[] { "ComId", "EmpId", "DtYear", "DtMonth" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummary_CompanyComId",
                table: "AttendanceSummary",
                column: "CompanyComId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummary_EmpId_ComId",
                table: "AttendanceSummary",
                columns: new[] { "EmpId", "ComId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummary_EmployeeEmpId_EmployeeComId",
                table: "AttendanceSummary",
                columns: new[] { "EmployeeEmpId", "EmployeeComId" });

            migrationBuilder.CreateIndex(
                name: "IX_Company_ComName",
                table: "Company",
                column: "ComName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_ComId",
                table: "Department",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DeptName",
                table: "Department",
                column: "DeptName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designation_ComId",
                table: "Designation",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_DesigName",
                table: "Designation",
                column: "DesigName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ComId",
                table: "Employee",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DeptId_ComId",
                table: "Employee",
                columns: new[] { "DeptId", "ComId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DesigId_ComId",
                table: "Employee",
                columns: new[] { "DesigId", "ComId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ShiftId_ComId",
                table: "Employee",
                columns: new[] { "ShiftId", "ComId" });

            migrationBuilder.CreateIndex(
                name: "IX_Salary_ComId_EmpId_DtYear_DtMonth",
                table: "Salary",
                columns: new[] { "ComId", "EmpId", "DtYear", "DtMonth" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salary_CompanyComId",
                table: "Salary",
                column: "CompanyComId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmpId_ComId",
                table: "Salary",
                columns: new[] { "EmpId", "ComId" });

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeEmpId_EmployeeComId",
                table: "Salary",
                columns: new[] { "EmployeeEmpId", "EmployeeComId" });

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ComId",
                table: "Shift",
                column: "ComId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "AttendanceSummary");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
