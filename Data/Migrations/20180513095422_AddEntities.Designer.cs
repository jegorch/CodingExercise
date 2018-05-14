﻿// <auto-generated />
using CodingExercise.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CodingExercise.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180513095422_AddEntities")]
    partial class AddEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CodingExercise.Model.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("CodingExercise.Model.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<int>("QuestionId");

                    b.Property<int>("SelectedAnswerId");

                    b.Property<int>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SelectedAnswerId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("CodingExercise.Model.Bundle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("AccountTypeId")
                        .HasColumnName("AccountTypeId");

                    b.Property<string>("BundleName")
                        .HasColumnName("BundleName");

                    b.Property<int>("Value")
                        .HasColumnName("Value");

                    b.HasKey("Id")
                        .HasName("PK_Bundle");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("Bundle");
                });

            modelBuilder.Entity("CodingExercise.Model.BundleRules", b =>
                {
                    b.Property<int>("BundleId")
                        .HasColumnName("BundleId");

                    b.Property<int>("PossibleAnswerId")
                        .HasColumnName("PossibleAnswerId");

                    b.HasKey("BundleId", "PossibleAnswerId")
                        .HasName("PK_BundleRules");

                    b.HasIndex("PossibleAnswerId");

                    b.ToTable("BundleRules");
                });

            modelBuilder.Entity("CodingExercise.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("AccountTypeId")
                        .HasColumnName("AccountTypeId");

                    b.Property<string>("CustomerName")
                        .HasColumnName("CustomerName");

                    b.HasKey("Id")
                        .HasName("PK_Customers");

                    b.HasIndex("AccountTypeId")
                        .HasName("FK_Customer_AccountType");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CodingExercise.Model.CustomerSurvey", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerId");

                    b.Property<int>("SurveyId")
                        .HasColumnName("SurveyId");

                    b.Property<DateTime>("Date")
                        .HasColumnName("Date");

                    b.HasKey("CustomerId", "SurveyId")
                        .HasName("PK_CustomerSurvey");

                    b.HasIndex("SurveyId");

                    b.ToTable("CustomerSurvey");
                });

            modelBuilder.Entity("CodingExercise.Model.PossibleAnswers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("PossibleAnswers");
                });

            modelBuilder.Entity("CodingExercise.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProductName");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CodingExercise.Model.ProductBundle", b =>
                {
                    b.Property<int>("BundleId")
                        .HasColumnName("BundleId");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductId");

                    b.HasKey("BundleId", "ProductId")
                        .HasName("PK_BundleProduct");

                    b.HasIndex("ProductId");

                    b.ToTable("BundleProduct");
                });

            modelBuilder.Entity("CodingExercise.Model.ProductRules", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnName("ProductId");

                    b.Property<int>("PossibleAnswerId")
                        .HasColumnName("PossibleAnswerId");

                    b.HasKey("ProductId", "PossibleAnswerId")
                        .HasName("PK_ProductRules");

                    b.HasIndex("PossibleAnswerId");

                    b.ToTable("ProductRules");
                });

            modelBuilder.Entity("CodingExercise.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("QuestionText");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("CodingExercise.Model.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .HasColumnName("Description");

                    b.Property<string>("Title")
                        .HasColumnName("Title");

                    b.HasKey("Id")
                        .HasName("PK_Survey");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("CodingExercise.Model.SurveyQuestion", b =>
                {
                    b.Property<int>("SurveyId")
                        .HasColumnName("SurveyId");

                    b.Property<int>("QuestionId")
                        .HasColumnName("QuestionId");

                    b.HasKey("SurveyId", "QuestionId")
                        .HasName("PK_SurveyQuestion");

                    b.HasIndex("QuestionId");

                    b.ToTable("SurveyQuestion");
                });

            modelBuilder.Entity("CodingExercise.Model.Answer", b =>
                {
                    b.HasOne("CodingExercise.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CodingExercise.Model.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CodingExercise.Model.PossibleAnswers", "SelectedAnswer")
                        .WithMany()
                        .HasForeignKey("SelectedAnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CodingExercise.Model.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CodingExercise.Model.Bundle", b =>
                {
                    b.HasOne("CodingExercise.Model.AccountType", "AccountType")
                        .WithMany("Bundles")
                        .HasForeignKey("AccountTypeId")
                        .HasConstraintName("FK_Bundle_AccountType")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CodingExercise.Model.BundleRules", b =>
                {
                    b.HasOne("CodingExercise.Model.Bundle", "Bundle")
                        .WithMany("Rules")
                        .HasForeignKey("BundleId")
                        .HasConstraintName("FK_BundleRules_Bundle")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CodingExercise.Model.PossibleAnswers", "PossibleAnswer")
                        .WithMany("BundlePossibleAnswers")
                        .HasForeignKey("PossibleAnswerId")
                        .HasConstraintName("FK_BundleRules_PossibleAnswers")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CodingExercise.Model.Customer", b =>
                {
                    b.HasOne("CodingExercise.Model.AccountType", "AccountType")
                        .WithMany("Customers")
                        .HasForeignKey("AccountTypeId")
                        .HasConstraintName("FK_Customers_AccountType")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CodingExercise.Model.CustomerSurvey", b =>
                {
                    b.HasOne("CodingExercise.Model.Customer", "Customer")
                        .WithMany("CustomerSurvey")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CodingExercise.Model.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CodingExercise.Model.PossibleAnswers", b =>
                {
                    b.HasOne("CodingExercise.Model.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CodingExercise.Model.ProductBundle", b =>
                {
                    b.HasOne("CodingExercise.Model.Bundle", "Bundle")
                        .WithMany("ProductIncluded")
                        .HasForeignKey("BundleId")
                        .HasConstraintName("FK_BundleProduct_Bundle")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CodingExercise.Model.Product", "Product")
                        .WithMany("Bundles")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_BundleProduct_Product")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CodingExercise.Model.ProductRules", b =>
                {
                    b.HasOne("CodingExercise.Model.PossibleAnswers", "PossibleAnswer")
                        .WithMany("ProductPossibleAnswers")
                        .HasForeignKey("PossibleAnswerId")
                        .HasConstraintName("FK_ProductRules_PossibleAnswers")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CodingExercise.Model.Product", "Product")
                        .WithMany("Rules")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductRules_Bundle")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CodingExercise.Model.SurveyQuestion", b =>
                {
                    b.HasOne("CodingExercise.Model.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CodingExercise.Model.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}