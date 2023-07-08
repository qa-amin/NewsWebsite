﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewsManagement.Infrastructure.EFCore;

#nullable disable

namespace NewsManagement.Infrastructure.EFCore.Migrations
{
    [DbContext(typeof(NewsManagementDbContext))]
    partial class NewsManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NewsManagement.Domain.NewsAgg.News", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsInternal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemove")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("NewsManagement.Domain.NewsLetterAgg.NewsLetter", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemove")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RegisterDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Email");

                    b.ToTable("NewsLetters");
                });

            modelBuilder.Entity("NewsManagement.Domain.NewsNewsCategoryAgg.NewsNewsCategory", b =>
                {
                    b.Property<int>("NewsCategoryId")
                        .HasColumnType("int");

                    b.Property<long>("NewsId")
                        .HasColumnType("bigint");

                    b.HasKey("NewsCategoryId", "NewsId");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsNewsCategories");
                });

            modelBuilder.Entity("NewsManagement.Domain.NewsTagAgg.NewsTag", b =>
                {
                    b.Property<long>("TagId")
                        .HasColumnType("bigint");

                    b.Property<long>("NewsId")
                        .HasColumnType("bigint");

                    b.HasKey("TagId", "NewsId");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsTags");
                });

            modelBuilder.Entity("NewsManagement.Domain.TagAgg.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemove")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("NewsManagement.Domain.VideoAgg.Video", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemove")
                        .HasColumnType("bit");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("NewsWebsite.Entities.NewsCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemove")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("NewsCategories");
                });

            modelBuilder.Entity("NewsManagement.Domain.NewsNewsCategoryAgg.NewsNewsCategory", b =>
                {
                    b.HasOne("NewsWebsite.Entities.NewsCategory", "NewsCategory")
                        .WithMany("NewsNewsCategories")
                        .HasForeignKey("NewsCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewsManagement.Domain.NewsAgg.News", "News")
                        .WithMany("NewsNewsCategories")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");

                    b.Navigation("NewsCategory");
                });

            modelBuilder.Entity("NewsManagement.Domain.NewsTagAgg.NewsTag", b =>
                {
                    b.HasOne("NewsManagement.Domain.NewsAgg.News", "News")
                        .WithMany("NewsTags")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewsManagement.Domain.TagAgg.Tag", "Tag")
                        .WithMany("NewsTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("NewsWebsite.Entities.NewsCategory", b =>
                {
                    b.HasOne("NewsWebsite.Entities.NewsCategory", "category")
                        .WithMany("Categories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("category");
                });

            modelBuilder.Entity("NewsManagement.Domain.NewsAgg.News", b =>
                {
                    b.Navigation("NewsNewsCategories");

                    b.Navigation("NewsTags");
                });

            modelBuilder.Entity("NewsManagement.Domain.TagAgg.Tag", b =>
                {
                    b.Navigation("NewsTags");
                });

            modelBuilder.Entity("NewsWebsite.Entities.NewsCategory", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("NewsNewsCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
