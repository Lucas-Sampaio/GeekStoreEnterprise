﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Carrinho.Api.Migrations
{
    public partial class cascadeDelte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItens_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItens");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItens_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItens",
                column: "CarrinhoId",
                principalTable: "CarrinhoCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItens_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItens");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItens_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItens",
                column: "CarrinhoId",
                principalTable: "CarrinhoCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
