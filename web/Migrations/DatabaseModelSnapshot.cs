﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web;

namespace web.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("web.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("web.DriverRace", b =>
                {
                    b.Property<Guid>("DriverId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RaceId")
                        .HasColumnType("char(36)");

                    b.HasKey("DriverId", "RaceId");

                    b.HasIndex("RaceId");

                    b.ToTable("DriverRace");
                });

            modelBuilder.Entity("web.Race", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BestTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("RaceCategory")
                        .HasColumnType("int");

                    b.Property<string>("WinnerName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Trials");
                });

            modelBuilder.Entity("web.RaceCar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CarType")
                        .HasColumnType("int");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Model")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TopSpeed")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("RaceCars");
                });

            modelBuilder.Entity("web.DriverRace", b =>
                {
                    b.HasOne("web.Driver", "Driver")
                        .WithMany("DriverRace")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("web.Race", "Race")
                        .WithMany("DriverRace")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Race");
                });

            modelBuilder.Entity("web.RaceCar", b =>
                {
                    b.HasOne("web.Driver", "Driver")
                        .WithMany("Cars")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("web.Driver", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("DriverRace");
                });

            modelBuilder.Entity("web.Race", b =>
                {
                    b.Navigation("DriverRace");
                });
#pragma warning restore 612, 618
        }
    }
}
