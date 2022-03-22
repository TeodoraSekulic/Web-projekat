﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace rent.Migrations
{
    [DbContext(typeof(FirmaContext))]
    partial class FirmaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Firma", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("ID");

                    b.ToTable("Firme");
                });

            modelBuilder.Entity("Models.Iznajmljivanje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FirmaID")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("VoziloID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FirmaID");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("VoziloID");

                    b.ToTable("Iznajmljivanja");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("FirmaID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("ID");

                    b.HasIndex("FirmaID");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Models.Vozilo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrojOcenjivanja")
                        .HasColumnType("int");

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<int?>("FirmeID")
                        .HasColumnType("int");

                    b.Property<bool>("Iznajmnjeno")
                        .HasColumnType("bit");

                    b.Property<string>("Marka")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Ocena")
                        .HasColumnType("float");

                    b.Property<string>("Tablice")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("ID");

                    b.HasIndex("FirmeID");

                    b.ToTable("Vozila");
                });

            modelBuilder.Entity("Models.Iznajmljivanje", b =>
                {
                    b.HasOne("Models.Firma", "Firma")
                        .WithMany("Iznajmljivanja")
                        .HasForeignKey("FirmaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Korisnik", "Korisnik")
                        .WithMany("Iznajmljivanja")
                        .HasForeignKey("KorisnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Vozilo", "Vozilo")
                        .WithMany("Iznajmljivanja")
                        .HasForeignKey("VoziloID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Firma");

                    b.Navigation("Korisnik");

                    b.Navigation("Vozilo");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.HasOne("Models.Firma", "Firma")
                        .WithMany("Korisnici")
                        .HasForeignKey("FirmaID");

                    b.Navigation("Firma");
                });

            modelBuilder.Entity("Models.Vozilo", b =>
                {
                    b.HasOne("Models.Firma", "Firme")
                        .WithMany("Vozila")
                        .HasForeignKey("FirmeID");

                    b.Navigation("Firme");
                });

            modelBuilder.Entity("Models.Firma", b =>
                {
                    b.Navigation("Iznajmljivanja");

                    b.Navigation("Korisnici");

                    b.Navigation("Vozila");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Navigation("Iznajmljivanja");
                });

            modelBuilder.Entity("Models.Vozilo", b =>
                {
                    b.Navigation("Iznajmljivanja");
                });
#pragma warning restore 612, 618
        }
    }
}