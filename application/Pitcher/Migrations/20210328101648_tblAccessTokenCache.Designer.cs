﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pitcher.Data;

namespace Pitcher.Migrations
{
    [DbContext(typeof(TeamContext))]
    [Migration("20210328101648_tblAccessTokenCache")]
    partial class tblAccessTokenCache
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("AccessTokenCache", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(449)
                        .HasColumnType("nvarchar(449)");

                    b.Property<DateTimeOffset?>("AbsoluteExpiration")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("ExpiresAtTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("SlidingExpirationInSeconds")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExpiresAtTime");

                    b.ToTable("AccessTokenCache", "security");
                });

            modelBuilder.Entity("Pitcher.Models.Chat", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ChatDescription")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ChatDescription");

                    b.Property<string>("ChatPublishDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProblemStartDate");

                    b.Property<int>("ProblemID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProblemID");

                    b.ToTable("tblChat");
                });

            modelBuilder.Entity("Pitcher.Models.Job", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("JobDeadline")
                        .HasColumnType("datetime2")
                        .HasColumnName("JobDeadlineDate");

                    b.Property<string>("JobDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("JobDescription");

                    b.Property<bool>("JobIsComplete")
                        .HasColumnType("bit")
                        .HasColumnName("JobIsComplete");

                    b.Property<DateTime>("JobStartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("JobStartDate");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("JobTitle");

                    b.HasKey("ID");

                    b.ToTable("tblJob");
                });

            modelBuilder.Entity("Pitcher.Models.Problem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("ProblemComplete")
                        .HasColumnType("bit")
                        .HasColumnName("ProblemComplete");

                    b.Property<string>("ProblemDescription")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProblemDescription");

                    b.Property<string>("ProblemFileAttachments")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProblemFileAttachments");

                    b.Property<int>("ProblemSeverity")
                        .HasColumnType("int")
                        .HasColumnName("ProblemSeverity");

                    b.Property<DateTime>("ProblemStartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ProblemStartDate");

                    b.Property<string>("ProblemTitle")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)")
                        .HasColumnName("ProblemTitle");

                    b.HasKey("ID");

                    b.ToTable("tblProblem");
                });

            modelBuilder.Entity("Pitcher.Models.Registration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("RegistrationDate");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("JobID");

                    b.HasIndex("UserID");

                    b.ToTable("tblRegistration");
                });

            modelBuilder.Entity("Pitcher.Models.Result", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.Property<int>("ProblemID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("JobID");

                    b.HasIndex("ProblemID");

                    b.ToTable("tblResult");
                });

            modelBuilder.Entity("Pitcher.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("UserAddress")
                        .HasMaxLength(37)
                        .HasColumnType("nvarchar(37)")
                        .HasColumnName("UserAddress");

                    b.Property<string>("UserContactEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("UserContactEmail");

                    b.Property<string>("UserCountry")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("UserCountry");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("UserFirstName");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("UserLastName");

                    b.Property<string>("UserMobileNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserMobileNumber");

                    b.Property<string>("UserPhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserPhoneNumber");

                    b.Property<string>("UserPostCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserPostCode");

                    b.Property<string>("UserState")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("UserState");

                    b.HasKey("ID");

                    b.ToTable("tblUser");
                });

            modelBuilder.Entity("Pitcher.Models.Chat", b =>
                {
                    b.HasOne("Pitcher.Models.Problem", "Problem")
                        .WithMany("Chat")
                        .HasForeignKey("ProblemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Pitcher.Models.Registration", b =>
                {
                    b.HasOne("Pitcher.Models.Job", "Job")
                        .WithMany("Registrations")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pitcher.Models.User", "User")
                        .WithMany("Registrations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pitcher.Models.Result", b =>
                {
                    b.HasOne("Pitcher.Models.Job", "Job")
                        .WithMany("Results")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pitcher.Models.Problem", "Problem")
                        .WithMany("Result")
                        .HasForeignKey("ProblemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Pitcher.Models.Job", b =>
                {
                    b.Navigation("Registrations");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("Pitcher.Models.Problem", b =>
                {
                    b.Navigation("Chat");

                    b.Navigation("Result");
                });

            modelBuilder.Entity("Pitcher.Models.User", b =>
                {
                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
