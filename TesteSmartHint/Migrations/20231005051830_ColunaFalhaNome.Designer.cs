﻿// <auto-generated />
using System;
using TesteSmartHint.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TesteSmartHint.Migrations
{
    [DbContext(typeof(TesteSmartHintContext))]
    [Migration("20231005051830_ColunaFalhaNome")]
    partial class ColunaFalhaNome
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TesteSmartHint.Models.Estacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Estacoes");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Falha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FlgAtivo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Falhas");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Filial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Filiais");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EstacaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EstacaoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("HoraFinal")
                        .HasColumnType("time(6)");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataAlteracaoSenha")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataUltimoAcesso")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FilialId")
                        .HasColumnType("int");

                    b.Property<bool>("FlgAtivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TurnoId")
                        .HasColumnType("int");

                    b.Property<string>("UltimaSenha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FilialId");

                    b.HasIndex("TurnoId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Verificacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EstacaoId")
                        .HasColumnType("int");

                    b.Property<int>("FalhaId")
                        .HasColumnType("int");

                    b.Property<string>("FalhaNome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstacaoId");

                    b.HasIndex("FalhaId");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Verificacoes");
                });

            modelBuilder.Entity("FalhaProduto", b =>
                {
                    b.Property<int>("FalhasId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutosId")
                        .HasColumnType("int");

                    b.HasKey("FalhasId", "ProdutosId");

                    b.HasIndex("ProdutosId");

                    b.ToTable("FalhaProduto");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Produto", b =>
                {
                    b.HasOne("TesteSmartHint.Models.Estacao", null)
                        .WithMany("Produtos")
                        .HasForeignKey("EstacaoId");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Usuario", b =>
                {
                    b.HasOne("TesteSmartHint.Models.Filial", "Filial")
                        .WithMany()
                        .HasForeignKey("FilialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TesteSmartHint.Models.Turno", "Turno")
                        .WithMany()
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filial");

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("TesteSmartHint.Models.Verificacao", b =>
                {
                    b.HasOne("TesteSmartHint.Models.Estacao", "Estacao")
                        .WithMany()
                        .HasForeignKey("EstacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TesteSmartHint.Models.Falha", "Falha")
                        .WithMany()
                        .HasForeignKey("FalhaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TesteSmartHint.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TesteSmartHint.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estacao");

                    b.Navigation("Falha");

                    b.Navigation("Produto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("FalhaProduto", b =>
                {
                    b.HasOne("TesteSmartHint.Models.Falha", null)
                        .WithMany()
                        .HasForeignKey("FalhasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TesteSmartHint.Models.Produto", null)
                        .WithMany()
                        .HasForeignKey("ProdutosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TesteSmartHint.Models.Estacao", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
