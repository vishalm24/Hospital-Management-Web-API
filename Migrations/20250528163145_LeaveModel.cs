using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management.Migrations
{
    /// <inheritdoc />
    public partial class LeaveModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_Doctors_DoctorId",
                table: "Leave");

            migrationBuilder.DropForeignKey(
                name: "FK_Leave_Users_AdminId",
                table: "Leave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leave",
                table: "Leave");

            migrationBuilder.RenameTable(
                name: "Leave",
                newName: "Leaves");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_DoctorId",
                table: "Leaves",
                newName: "IX_Leaves_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Leave_AdminId",
                table: "Leaves",
                newName: "IX_Leaves_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leaves",
                table: "Leaves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Doctors_DoctorId",
                table: "Leaves",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Users_AdminId",
                table: "Leaves",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Doctors_DoctorId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Users_AdminId",
                table: "Leaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leaves",
                table: "Leaves");

            migrationBuilder.RenameTable(
                name: "Leaves",
                newName: "Leave");

            migrationBuilder.RenameIndex(
                name: "IX_Leaves_DoctorId",
                table: "Leave",
                newName: "IX_Leave_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Leaves_AdminId",
                table: "Leave",
                newName: "IX_Leave_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leave",
                table: "Leave",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_Doctors_DoctorId",
                table: "Leave",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_Users_AdminId",
                table: "Leave",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
