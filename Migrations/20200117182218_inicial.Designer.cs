﻿// <auto-generated />
using System;
using ControleFaturamentoJnx.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControleFaturamentoJnx.Migrations
{
    [DbContext(typeof(ControleFaturamentoContext))]
    [Migration("20200117182218_inicial")]
    partial class inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Cliente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Documento")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NumeroDocumento")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Despesa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TipoCustoID")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("ID");

                    b.HasIndex("TipoCustoID");

                    b.ToTable("Despesa");
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Endereco", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Cep")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Cidade")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Numero")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Rua")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("ClienteID")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Fatura", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEmissao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Numero")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("NumeroParcelas")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("ValorFatura")
                        .HasColumnType("double");

                    b.HasKey("ID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Faturas");
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Parcela", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FaturaID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("ValorParcela")
                        .HasColumnType("double");

                    b.Property<DateTime>("Vencimento")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("FaturaID");

                    b.ToTable("Parcelas");
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.TipoCusto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("TipoCusto");
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Despesa", b =>
                {
                    b.HasOne("ControleFaturamentoJnx.Models.TipoCusto", "TipoCusto")
                        .WithMany("Despesas")
                        .HasForeignKey("TipoCustoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Endereco", b =>
                {
                    b.HasOne("ControleFaturamentoJnx.Models.Cliente", "Cliente")
                        .WithOne("Endereco")
                        .HasForeignKey("ControleFaturamentoJnx.Models.Endereco", "ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Fatura", b =>
                {
                    b.HasOne("ControleFaturamentoJnx.Models.Cliente", "Cliente")
                        .WithMany("Faturas")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControleFaturamentoJnx.Models.Parcela", b =>
                {
                    b.HasOne("ControleFaturamentoJnx.Models.Fatura", "Fatura")
                        .WithMany("Parcelas")
                        .HasForeignKey("FaturaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}