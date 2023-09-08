﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ToDoApp.Data;

#nullable disable

namespace ToDoApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230907154512_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ToDoApp.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatumUstvarjanja")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("datum_ustvarjanja");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("naslov");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("opis");

                    b.Property<bool>("Opravljeno")
                        .HasColumnType("boolean")
                        .HasColumnName("opravljeno");

                    b.HasKey("Id");

                    b.ToTable("tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
