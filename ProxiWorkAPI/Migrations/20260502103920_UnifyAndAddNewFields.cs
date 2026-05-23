using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProxiWorkAPI.Migrations
{
    /// <inheritdoc />
    public partial class UnifyAndAddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "UserType");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "WorkTime",
                table: "Jobs",
                newName: "WorkingHours");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Applications",
                newName: "ApplicantId");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Applications",
                newName: "CoverMessage");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Applications",
                newName: "AppliedAt");

            migrationBuilder.RenameColumn(
                name: "CV",
                table: "Applications",
                newName: "CvImageUrl");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                newName: "IX_Applications_ApplicantId");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeenAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "RadiusKm",
                table: "Jobs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RequiredSkills",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VacanciesCount",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DistanceKm",
                table: "Applications",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployerNote",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_ApplicantId",
                table: "Applications",
                column: "ApplicantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_ApplicantId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastSeenAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "RadiusKm",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "RequiredSkills",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "VacanciesCount",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DistanceKm",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EmployerNote",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "WorkingHours",
                table: "Jobs",
                newName: "WorkTime");

            migrationBuilder.RenameColumn(
                name: "CvImageUrl",
                table: "Applications",
                newName: "CV");

            migrationBuilder.RenameColumn(
                name: "CoverMessage",
                table: "Applications",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "AppliedAt",
                table: "Applications",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "Applications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicantId",
                table: "Applications",
                newName: "IX_Applications_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
