﻿// <auto-generated />
using System;
using AutomatedFinances.Core.MigrationInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutomatedFinances.Core.Migrations
{
    [DbContext(typeof(IridiumDbMigrationContext))]
    [Migration("20220615062659_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AutomatedFinances.Core.Entities.GenericTransaction", b =>
                {
                    b.Property<Guid>("TrackingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccuredAtDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordedAtDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SavedAtDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SavedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrackingId");

                    b.ToTable("GenericTransaction", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}