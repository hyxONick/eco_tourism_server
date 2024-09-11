﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eco_tourism_gateway.DB;

#nullable disable

namespace eco_tourism_gateway.Migrations
{
    [DbContext(typeof(EcoEventLogContext))]
    [Migration("20240911010912_UpdateEventLogModel")]
    partial class UpdateEventLogModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("eco_tourism_gateway.DB.EventLog", b =>
                {
                    b.Property<int>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CaseId"));

                    b.Property<string>("Resource")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TaskId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CaseId");

                    b.ToTable("eco_tourism_tourist_EventLog", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
