﻿// <auto-generated />
using System;
using LockManager.Infrastructure.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LockManager.Infrastructure.Migrations
{
    [DbContext(typeof(LockManagerDbContext))]
    [Migration("20220825003834_door-history")]
    partial class doorhistory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LockManager.Domain.Entities.Door", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MinimumRoleAuthorized")
                        .HasColumnType("int")
                        .HasColumnName("MinimumRoleAuthorized");

                    b.Property<bool>("Open")
                        .HasColumnType("bit")
                        .HasColumnName("Open");

                    b.HasKey("Id");

                    b.ToTable("Door", (string)null);
                });

            modelBuilder.Entity("LockManager.Domain.Entities.DoorHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DoorId")
                        .HasColumnType("int")
                        .HasColumnName("DoorId");

                    b.Property<DateTime>("EntryDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("EntryDateTime");

                    b.Property<bool>("IsSuccessfulEntry")
                        .HasColumnType("bit")
                        .HasColumnName("IsSuccessfulEntry");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DoorId" }, "IX_Door");

                    b.HasIndex(new[] { "UserId" }, "IX_User");

                    b.ToTable("DoorHistory", (string)null);
                });

            modelBuilder.Entity("LockManager.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("Active");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("Role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Username" }, "IX_Username")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("LockManager.Domain.Entities.UserAuth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordHash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordSalt");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RefreshToken");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("TokenCreated");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2")
                        .HasColumnName("TokenExpires");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Username" }, "IX_Username")
                        .IsUnique()
                        .HasDatabaseName("IX_Username1");

                    b.ToTable("UserAuth", (string)null);
                });

            modelBuilder.Entity("LockManager.Domain.Entities.DoorHistory", b =>
                {
                    b.HasOne("LockManager.Domain.Entities.Door", "Door")
                        .WithMany("DoorHistoryList")
                        .HasForeignKey("DoorId")
                        .IsRequired()
                        .HasConstraintName("FK_Door_Id");

                    b.Navigation("Door");
                });

            modelBuilder.Entity("LockManager.Domain.Entities.Door", b =>
                {
                    b.Navigation("DoorHistoryList");
                });
#pragma warning restore 612, 618
        }
    }
}