using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CodingExercise.Data.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bundle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountTypeId = table.Column<int>(nullable: false),
                    BundleName = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundle", x => x.Id);
                    //table.ForeignKey(
                    //    name: "FK_Bundle_AccountType",
                    //    column: x => x.AccountTypeId,
                    //    principalTable: "AccountTypes",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountTypeId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AccountType",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PossibleAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossibleAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PossibleAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestion",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestion", x => new { x.SurveyId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_SurveyQuestion_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyQuestion_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BundleProduct",
                columns: table => new
                {
                    BundleId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleProduct", x => new { x.BundleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_BundleProduct_Bundle",
                        column: x => x.BundleId,
                        principalTable: "Bundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BundleProduct_Product",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSurvey",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSurvey", x => new { x.CustomerId, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_CustomerSurvey_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSurvey_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    SelectedAnswerId = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Answers_PossibleAnswers_SelectedAnswerId",
                        column: x => x.SelectedAnswerId,
                        principalTable: "PossibleAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Answers_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BundleRules",
                columns: table => new
                {
                    BundleId = table.Column<int>(nullable: false),
                    PossibleAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleRules", x => new { x.BundleId, x.PossibleAnswerId });
                    table.ForeignKey(
                        name: "FK_BundleRules_Bundle",
                        column: x => x.BundleId,
                        principalTable: "Bundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BundleRules_PossibleAnswers",
                        column: x => x.PossibleAnswerId,
                        principalTable: "PossibleAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductRules",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    PossibleAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRules", x => new { x.ProductId, x.PossibleAnswerId });
                    table.ForeignKey(
                        name: "FK_ProductRules_PossibleAnswers",
                        column: x => x.PossibleAnswerId,
                        principalTable: "PossibleAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRules_Bundle",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CustomerId",
                table: "Answers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SelectedAnswerId",
                table: "Answers",
                column: "SelectedAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyId",
                table: "Answers",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bundle_AccountTypeId",
                table: "Bundle",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleProduct_ProductId",
                table: "BundleProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleRules_PossibleAnswerId",
                table: "BundleRules",
                column: "PossibleAnswerId");

            migrationBuilder.CreateIndex(
                name: "FK_Customer_AccountType",
                table: "Customers",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSurvey_SurveyId",
                table: "CustomerSurvey",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_PossibleAnswers_QuestionId",
                table: "PossibleAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRules_PossibleAnswerId",
                table: "ProductRules",
                column: "PossibleAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_QuestionId",
                table: "SurveyQuestion",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "BundleProduct");

            migrationBuilder.DropTable(
                name: "BundleRules");

            migrationBuilder.DropTable(
                name: "CustomerSurvey");

            migrationBuilder.DropTable(
                name: "ProductRules");

            migrationBuilder.DropTable(
                name: "SurveyQuestion");

            migrationBuilder.DropTable(
                name: "Bundle");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PossibleAnswers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
