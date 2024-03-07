﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projekt_webbservice.Data;

#nullable disable

namespace projekt_webbservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240306143713_UpdatedAudioModel")]
    partial class UpdatedAudioModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("AudioUser", b =>
                {
                    b.Property<int>("AudiosAudioID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AudiosAudioID", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("AudioUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("project_webbservice.Models.Audio", b =>
                {
                    b.Property<int>("AudioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("AudioData")
                        .HasColumnType("BLOB");

                    b.Property<int>("CategoryID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageNameOriginal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UploaderUser")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("VideoData")
                        .HasColumnType("BLOB");

                    b.HasKey("AudioID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Audio");
                });

            modelBuilder.Entity("project_webbservice.Models.Avatar", b =>
                {
                    b.Property<int>("AvatarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AvatarImageName")
                        .HasColumnType("TEXT");

                    b.HasKey("AvatarId");

                    b.ToTable("Avatar");
                });

            modelBuilder.Entity("project_webbservice.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("project_webbservice.Models.Like", b =>
                {
                    b.Property<int>("LikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AudioID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LikedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LikeID");

                    b.HasIndex("AudioID");

                    b.HasIndex("UserID");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("project_webbservice.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AvatarId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserInfo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("AvatarId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("project_webbservice.Models.UserAudio", b =>
                {
                    b.Property<int>("UserAudioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AudioId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserAudioId");

                    b.HasIndex("AudioId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAudio");
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("AudioUser", b =>
                {
                    b.HasOne("project_webbservice.Models.Audio", null)
                        .WithMany()
                        .HasForeignKey("AudiosAudioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_webbservice.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("project_webbservice.Models.Audio", b =>
                {
                    b.HasOne("project_webbservice.Models.Category", "Category")
                        .WithMany("Audios")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("project_webbservice.Models.Like", b =>
                {
                    b.HasOne("project_webbservice.Models.Audio", "Audio")
                        .WithMany("Likes")
                        .HasForeignKey("AudioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_webbservice.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("project_webbservice.Models.User", b =>
                {
                    b.HasOne("project_webbservice.Models.Avatar", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("project_webbservice.Models.UserAudio", b =>
                {
                    b.HasOne("project_webbservice.Models.Audio", "Audio")
                        .WithMany("UserAudios")
                        .HasForeignKey("AudioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_webbservice.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("project_webbservice.Models.Audio", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("UserAudios");
                });

            modelBuilder.Entity("project_webbservice.Models.Category", b =>
                {
                    b.Navigation("Audios");
                });

            modelBuilder.Entity("project_webbservice.Models.User", b =>
                {
                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
