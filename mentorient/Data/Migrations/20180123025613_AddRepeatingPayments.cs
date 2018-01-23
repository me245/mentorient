using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace mentorient.Data.Migrations
{
    public partial class AddRepeatingPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountRemaining",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "TenantName",
                table: "Entries");

            migrationBuilder.AddColumn<int>(
                name: "RepeatingPaymentId",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPaid",
                table: "Entries",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDue",
                table: "Entries",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateTable(
                name: "RepeatingPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountDue = table.Column<decimal>(nullable: false),
                    DayOfMonthDue = table.Column<int>(nullable: false),
                    DayToGenerateNewPayment = table.Column<int>(nullable: false),
                    RepeatedPaymentEndDate = table.Column<DateTime>(nullable: true),
                    RepeatedPaymentStartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatingPayment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_RepeatingPaymentId",
                table: "Tenants",
                column: "RepeatingPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_RepeatingPayment_RepeatingPaymentId",
                table: "Tenants",
                column: "RepeatingPaymentId",
                principalTable: "RepeatingPayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_RepeatingPayment_RepeatingPaymentId",
                table: "Tenants");

            migrationBuilder.DropTable(
                name: "RepeatingPayment");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_RepeatingPaymentId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "RepeatingPaymentId",
                table: "Tenants");

            migrationBuilder.AlterColumn<double>(
                name: "AmountPaid",
                table: "Entries",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "AmountDue",
                table: "Entries",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<double>(
                name: "AmountRemaining",
                table: "Entries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TenantName",
                table: "Entries",
                nullable: true);
        }
    }
}
