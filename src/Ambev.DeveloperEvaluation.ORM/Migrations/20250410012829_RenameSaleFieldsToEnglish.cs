using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class RenameSaleFieldsToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "Sales",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "NumeroVenda",
                table: "Sales",
                newName: "SaleNumber");

            migrationBuilder.RenameColumn(
                name: "FilialNome",
                table: "Sales",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "FilialId",
                table: "Sales",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "DataVenda",
                table: "Sales",
                newName: "SaleDate");

            migrationBuilder.RenameColumn(
                name: "ClienteNome",
                table: "Sales",
                newName: "BranchName");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Sales",
                newName: "BranchId");

            migrationBuilder.RenameColumn(
                name: "Cancelada",
                table: "Sales",
                newName: "IsCancelled");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "SaleItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ProdutoNome",
                table: "SaleItems",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "SaleItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "PrecoUnitario",
                table: "SaleItems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Desconto",
                table: "SaleItems",
                newName: "Discount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Sales",
                newName: "ValorTotal");

            migrationBuilder.RenameColumn(
                name: "SaleNumber",
                table: "Sales",
                newName: "NumeroVenda");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Sales",
                newName: "DataVenda");

            migrationBuilder.RenameColumn(
                name: "IsCancelled",
                table: "Sales",
                newName: "Cancelada");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Sales",
                newName: "FilialNome");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Sales",
                newName: "FilialId");

            migrationBuilder.RenameColumn(
                name: "BranchName",
                table: "Sales",
                newName: "ClienteNome");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Sales",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "SaleItems",
                newName: "PrecoUnitario");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SaleItems",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "SaleItems",
                newName: "ProdutoNome");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SaleItems",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "SaleItems",
                newName: "Desconto");
        }
    }
}
