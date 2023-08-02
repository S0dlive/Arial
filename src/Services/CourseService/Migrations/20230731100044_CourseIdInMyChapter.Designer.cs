﻿// <auto-generated />
using System;
using CourseService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseService.Migrations
{
    [DbContext(typeof(CourseDbContext))]
    [Migration("20230731100044_CourseIdInMyChapter")]
    partial class CourseIdInMyChapter
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CourseService.Models.Chapter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ChapterContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ChapterName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LittleDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("CourseService.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<short>("StarsNumber")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CourseService.Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseService.Models.CourseDeletor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ExpireIn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("CourseDeletors");
                });

            modelBuilder.Entity("CourseService.Models.LikeComment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CommentId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LikorId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LikeComments");
                });
#pragma warning restore 612, 618
        }
    }
}