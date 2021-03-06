﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20200614193933_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Data.Models.CourseEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("30fd7c5b-8181-4cc7-8f4d-58a122c077d0"),
                            Title = "Math"
                        },
                        new
                        {
                            Id = new Guid("d6221796-b017-4bd2-b7c8-bf59b7e2cd41"),
                            Title = "Programming"
                        });
                });

            modelBuilder.Entity("Data.Models.CourseStudentEntity", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseStudents");

                    b.HasData(
                        new
                        {
                            StudentId = new Guid("a461a090-434c-414a-a188-295207a10b04"),
                            CourseId = new Guid("30fd7c5b-8181-4cc7-8f4d-58a122c077d0")
                        },
                        new
                        {
                            StudentId = new Guid("9505979e-3f4b-4f20-88ce-9a3069fa6d51"),
                            CourseId = new Guid("d6221796-b017-4bd2-b7c8-bf59b7e2cd41")
                        },
                        new
                        {
                            StudentId = new Guid("a461a090-434c-414a-a188-295207a10b04"),
                            CourseId = new Guid("d6221796-b017-4bd2-b7c8-bf59b7e2cd41")
                        });
                });

            modelBuilder.Entity("Data.Models.StudentEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9505979e-3f4b-4f20-88ce-9a3069fa6d51"),
                            FirstName = "Sviat",
                            LastName = "Laskov"
                        },
                        new
                        {
                            Id = new Guid("a461a090-434c-414a-a188-295207a10b04"),
                            FirstName = "Ivan",
                            LastName = "Petrov"
                        });
                });

            modelBuilder.Entity("Data.Models.CourseStudentEntity", b =>
                {
                    b.HasOne("Data.Models.CourseEntity", "Course")
                        .WithMany("CourseStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.StudentEntity", "Student")
                        .WithMany("CourseStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
