﻿// <auto-generated />
using System;
using AwesomeGICBank.SqlServerPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AwesomeGICBank.SqlServerPersistence.Migrations
{
    [DbContext(typeof(AwesomeGICBankDbContext))]
    [Migration("20241222072425_Add_Account_and_Transaction")]
    partial class Add_Account_and_Transaction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AwesomeGICBank.Domain.Entities.Account", b =>
                {
                    b.Property<string>("AccountNo")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("AccountNo");

                    b.ToTable("Accounts", "acc");
                });

            modelBuilder.Entity("AwesomeGICBank.Domain.ValueObjects.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("AccountNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Type")
                        .HasMaxLength(1)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountNo");

                    b.ToTable("Transactions", "acc");
                });

            modelBuilder.Entity("AwesomeGICBank.Domain.ValueObjects.Transaction", b =>
                {
                    b.HasOne("AwesomeGICBank.Domain.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AwesomeGICBank.Domain.Entities.Account", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
