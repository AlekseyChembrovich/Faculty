﻿// <auto-generated />
using System;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Faculty.DataAccessLayer.Migrations
{
    [DbContext(typeof(DatabaseContextEntityFramework))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Curator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Doublename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Curators");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountYearEducation")
                        .HasColumnType("int");

                    b.Property<int?>("CuratorId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDateEducation")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpecializationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialozations");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Doublename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Faculty", b =>
                {
                    b.HasOne("Faculty.DataAccessLayer.Models.Curator", "Curator")
                        .WithMany("Faculties")
                        .HasForeignKey("CuratorId");

                    b.HasOne("Faculty.DataAccessLayer.Models.Group", "Group")
                        .WithMany("Faculties")
                        .HasForeignKey("GroupId");

                    b.HasOne("Faculty.DataAccessLayer.Models.Student", "Student")
                        .WithMany("Faculties")
                        .HasForeignKey("StudentId");

                    b.Navigation("Curator");

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Group", b =>
                {
                    b.HasOne("Faculty.DataAccessLayer.Models.Specialization", "Specialization")
                        .WithMany("Groups")
                        .HasForeignKey("SpecializationId");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Curator", b =>
                {
                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Group", b =>
                {
                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Specialization", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Faculty.DataAccessLayer.Models.Student", b =>
                {
                    b.Navigation("Faculties");
                });
#pragma warning restore 612, 618
        }
    }
}
