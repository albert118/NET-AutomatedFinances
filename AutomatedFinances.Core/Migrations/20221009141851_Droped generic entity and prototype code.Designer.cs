﻿// <auto-generated />
using System;
using AutomatedFinances.Core.MigrationInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutomatedFinances.Core.Migrations
{
    [DbContext(typeof(IridiumDbMigrationContext))]
    [Migration("20221009141851_Droped generic entity and prototype code")]
    partial class Dropedgenericentityandprototypecode
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AutomatedFinances.Core.Entities.FinancialTransactionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("OccuredAtDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordedAtDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("SavedAtDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SavedBy")
                        .HasColumnType("longtext");

                    b.Property<long>("TotalCost")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("FinancialTransactionRecord", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
