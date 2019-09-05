﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTask.Context;

namespace TestTask.Migrations
{
    [DbContext(typeof(MedContext))]
    partial class MedContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestTask.Context.Graft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Consent");

                    b.Property<string>("Drug");

                    b.Property<DateTime>("EventDate");

                    b.Property<int>("PatientId");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Grafts");
                });

            modelBuilder.Entity("TestTask.Context.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Patronymic");

                    b.Property<string>("SNILS");

                    b.Property<int>("Sex");

                    b.Property<string>("Surname")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("TestTask.Context.Graft", b =>
                {
                    b.HasOne("TestTask.Context.Patient", "Patient")
                        .WithMany("Grafts")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
