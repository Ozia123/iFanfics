﻿// <auto-generated />
using iFanfics.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace iFanfics.Web.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180224012213_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iFanfics.DAL.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Chapter", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChapterNumber");

                    b.Property<string>("ChapterText")
                        .IsRequired();

                    b.Property<string>("FanficId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FanficId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.ChapterRating", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("ChapterId");

                    b.Property<int>("GivenRating");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ChapterId");

                    b.ToTable("ChaptersRating");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.ClientProfile", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTime>("DateOfCreation");

                    b.Property<string>("PictureURL");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ClientProfiles");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("CommentText")
                        .IsRequired();

                    b.Property<string>("FanficId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("FanficId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.CommentRating", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("CommentId");

                    b.Property<int>("GivenRating");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CommentId");

                    b.ToTable("CommentsRating");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Fanfic", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DateOfCreation");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("GenreId");

                    b.Property<DateTime>("LastModifyingDate");

                    b.Property<string>("PictureURL");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("GenreId");

                    b.ToTable("Fanfics");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.FanficTags", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FanficId");

                    b.Property<string>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("FanficId");

                    b.ToTable("FanficsTags");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Genre", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GenreName");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Tag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagName");

                    b.Property<int>("Uses");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Chapter", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.Fanfic")
                        .WithMany("Chapters")
                        .HasForeignKey("FanficId");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.ChapterRating", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany("ChaptersRating")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("iFanfics.DAL.Entities.Chapter")
                        .WithMany("ChapterRating")
                        .HasForeignKey("ChapterId");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.ClientProfile", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("ClientProfile")
                        .HasForeignKey("iFanfics.DAL.Entities.ClientProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Comment", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany("Comments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("iFanfics.DAL.Entities.Fanfic")
                        .WithMany("Comments")
                        .HasForeignKey("FanficId");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.CommentRating", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany("CommentsRating")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("iFanfics.DAL.Entities.Comment")
                        .WithMany("CommentRating")
                        .HasForeignKey("CommentId");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.Fanfic", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany("Fanfics")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("iFanfics.DAL.Entities.Genre")
                        .WithMany("Fanfics")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("iFanfics.DAL.Entities.FanficTags", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.Fanfic")
                        .WithMany("FanficTags")
                        .HasForeignKey("FanficId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("iFanfics.DAL.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
