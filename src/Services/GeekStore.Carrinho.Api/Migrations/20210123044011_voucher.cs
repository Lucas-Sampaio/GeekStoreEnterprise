﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Carrinho.Api.Migrations
{
    public partial class voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "CarrinhoCliente",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentual",
                table: "CarrinhoCliente",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoDesconto",
                table: "CarrinhoCliente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDesconto",
                table: "CarrinhoCliente",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCodigo",
                table: "CarrinhoCliente",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VoucherUtilizado",
                table: "CarrinhoCliente",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "CarrinhoCliente");

            migrationBuilder.DropColumn(
                name: "Percentual",
                table: "CarrinhoCliente");

            migrationBuilder.DropColumn(
                name: "TipoDesconto",
                table: "CarrinhoCliente");

            migrationBuilder.DropColumn(
                name: "ValorDesconto",
                table: "CarrinhoCliente");

            migrationBuilder.DropColumn(
                name: "VoucherCodigo",
                table: "CarrinhoCliente");

            migrationBuilder.DropColumn(
                name: "VoucherUtilizado",
                table: "CarrinhoCliente");
        }
    }
}
