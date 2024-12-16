﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeWEB.Models;

#nullable disable

namespace RecipeWEB.Migrations
{
    [DbContext(typeof(RecipeContext))]
    [Migration("20241216105232_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecipeDiet", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.Property<int>("DietId")
                        .HasColumnType("int")
                        .HasColumnName("DietID");

                    b.HasKey("RecipeId", "DietId")
                        .HasName("PK__RecipeDi__076DA7BB24B9D8E1");

                    b.HasIndex("DietId");

                    b.ToTable("RecipeDiets", (string)null);
                });

            modelBuilder.Entity("RecipeTool", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.Property<int>("ToolId")
                        .HasColumnType("int")
                        .HasColumnName("ToolID");

                    b.HasKey("RecipeId", "ToolId")
                        .HasName("PK__RecipeTo__F119466B13BD6EBE");

                    b.HasIndex("ToolId");

                    b.ToTable("RecipeTools", (string)null);
                });

            modelBuilder.Entity("RecipeWEB.Models.Allergen", b =>
                {
                    b.Property<int>("AllergenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AllergenID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AllergenId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("AllergenId")
                        .HasName("PK__Allergen__158B937FA014BFB5");

                    b.HasIndex(new[] { "Name" }, "UQ__Allergen__737584F65FE771EC")
                        .IsUnique();

                    b.ToTable("Allergens");
                });

            modelBuilder.Entity("RecipeWEB.Models.Diet", b =>
                {
                    b.Property<int>("DietId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DietID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DietId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("DietId")
                        .HasName("PK__Diets__AB42F6BECD9C9971");

                    b.HasIndex(new[] { "Name" }, "UQ__Diets__737584F6DC72AD88")
                        .IsUnique();

                    b.ToTable("Diets");
                });

            modelBuilder.Entity("RecipeWEB.Models.FavoriteRecipe", b =>
                {
                    b.Property<int>("FavoriteRecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FavoriteRecipeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavoriteRecipeId"));

                    b.Property<DateTime?>("AddedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("FavoriteRecipeId")
                        .HasName("PK__Favorite__5CEF438B9986D8BF");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteRecipes");
                });

            modelBuilder.Entity("RecipeWEB.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IngredientID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<int?>("AllergenId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IngredientId")
                        .HasName("PK__Ingredie__BEAEB27A3F93C860");

                    b.HasIndex("AllergenId");

                    b.HasIndex(new[] { "Name" }, "UQ__Ingredie__737584F68A190DD6")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RecipeWEB.Models.KitchenTool", b =>
                {
                    b.Property<int>("ToolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ToolID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ToolId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ToolId")
                        .HasName("PK__KitchenT__CC0CEBB12266E1BA");

                    b.HasIndex(new[] { "Name" }, "UQ__KitchenT__737584F6BFD9949C")
                        .IsUnique();

                    b.ToTable("KitchenTools");
                });

            modelBuilder.Entity("RecipeWEB.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<int?>("CookTime")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ImageURL");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PrepTime")
                        .HasColumnType("int");

                    b.Property<int?>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("RecipeId")
                        .HasName("PK__Recipes__FDD988D0CD1CECE1");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipeWEB.Models.RecipeComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CommentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CommentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CommentId")
                        .HasName("PK__RecipeCo__C3B4DFAA86ECAAD6");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("RecipeComments");
                });

            modelBuilder.Entity("RecipeWEB.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int")
                        .HasColumnName("IngredientID");

                    b.Property<string>("Quantity")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("RecipeId", "IngredientId")
                        .HasName("PK__RecipeIn__463363F775C2FCDB");

                    b.HasIndex("IngredientId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("RecipeWEB.Models.RecipeRating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RatingID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RatingDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeID");

                    b.Property<string>("Review")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("RatingId")
                        .HasName("PK__RecipeRa__FCCDF85C5FE0DE0F");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("RecipeRatings");
                });

            modelBuilder.Entity("RecipeWEB.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreateByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("RecipeWEB.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCAC274802CD");

                    b.HasIndex(new[] { "Username" }, "UQ__Users__536C85E43A8DBE87")
                        .IsUnique();

                    b.HasIndex(new[] { "Email" }, "UQ__Users__A9D1053403F025B4")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecipeDiet", b =>
                {
                    b.HasOne("RecipeWEB.Models.Diet", null)
                        .WithMany()
                        .HasForeignKey("DietId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeDie__DietI__5224328E");

                    b.HasOne("RecipeWEB.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeDie__Recip__51300E55");
                });

            modelBuilder.Entity("RecipeTool", b =>
                {
                    b.HasOne("RecipeWEB.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeToo__Recip__55009F39");

                    b.HasOne("RecipeWEB.Models.KitchenTool", null)
                        .WithMany()
                        .HasForeignKey("ToolId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeToo__ToolI__55F4C372");
                });

            modelBuilder.Entity("RecipeWEB.Models.FavoriteRecipe", b =>
                {
                    b.HasOne("RecipeWEB.Models.Recipe", "Recipe")
                        .WithMany("FavoriteRecipes")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK__FavoriteR__Recip__4E53A1AA");

                    b.HasOne("RecipeWEB.Models.User", "User")
                        .WithMany("FavoriteRecipes")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__FavoriteR__UserI__4D5F7D71");

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeWEB.Models.Ingredient", b =>
                {
                    b.HasOne("RecipeWEB.Models.Allergen", "Allergen")
                        .WithMany("Ingredients")
                        .HasForeignKey("AllergenId")
                        .HasConstraintName("FK__Ingredien__Aller__3B40CD36");

                    b.Navigation("Allergen");
                });

            modelBuilder.Entity("RecipeWEB.Models.Recipe", b =>
                {
                    b.HasOne("RecipeWEB.Models.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Recipes__UserID__3493CFA7");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeWEB.Models.RecipeComment", b =>
                {
                    b.HasOne("RecipeWEB.Models.Recipe", "Recipe")
                        .WithMany("RecipeComments")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeCom__Recip__489AC854");

                    b.HasOne("RecipeWEB.Models.User", "User")
                        .WithMany("RecipeComments")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeCom__UserI__498EEC8D");

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeWEB.Models.RecipeIngredient", b =>
                {
                    b.HasOne("RecipeWEB.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeIng__Ingre__3F115E1A");

                    b.HasOne("RecipeWEB.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeIng__Recip__3E1D39E1");

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeWEB.Models.RecipeRating", b =>
                {
                    b.HasOne("RecipeWEB.Models.Recipe", "Recipe")
                        .WithMany("RecipeRatings")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeRat__Recip__43D61337");

                    b.HasOne("RecipeWEB.Models.User", "User")
                        .WithMany("RecipeRatings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__RecipeRat__UserI__44CA3770");

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeWEB.Models.RefreshToken", b =>
                {
                    b.HasOne("RecipeWEB.Models.User", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("RecipeWEB.Models.Allergen", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("RecipeWEB.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("RecipeWEB.Models.Recipe", b =>
                {
                    b.Navigation("FavoriteRecipes");

                    b.Navigation("RecipeComments");

                    b.Navigation("RecipeIngredients");

                    b.Navigation("RecipeRatings");
                });

            modelBuilder.Entity("RecipeWEB.Models.User", b =>
                {
                    b.Navigation("FavoriteRecipes");

                    b.Navigation("RecipeComments");

                    b.Navigation("RecipeRatings");

                    b.Navigation("Recipes");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
