using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecipeWEB.Models;

public partial class RecipeContext : DbContext
{
    public RecipeContext()
    {
    }

    public RecipeContext(DbContextOptions<RecipeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergen> Allergens { get; set; }

    public virtual DbSet<Diet> Diets { get; set; }

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<KitchenTool> KitchenTools { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeComment> RecipeComments { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<RecipeRating> RecipeRatings { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergen>(entity =>
        {
            entity.HasKey(e => e.AllergenId).HasName("PK__Allergen__158B937FA014BFB5");

            entity.HasIndex(e => e.Name, "UQ__Allergen__737584F65FE771EC").IsUnique();

            entity.Property(e => e.AllergenId).HasColumnName("AllergenID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Diet>(entity =>
        {
            entity.HasKey(e => e.DietId).HasName("PK__Diets__AB42F6BECD9C9971");

            entity.HasIndex(e => e.Name, "UQ__Diets__737584F6DC72AD88").IsUnique();

            entity.Property(e => e.DietId).HasColumnName("DietID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.FavoriteRecipeId).HasName("PK__Favorite__5CEF438B9986D8BF");

            entity.Property(e => e.FavoriteRecipeId).HasColumnName("FavoriteRecipeID");
            entity.Property(e => e.AddedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteR__Recip__4E53A1AA");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteR__UserI__4D5F7D71");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB27A3F93C860");

            entity.HasIndex(e => e.Name, "UQ__Ingredie__737584F68A190DD6").IsUnique();

            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Allergen).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.AllergenId)
                .HasConstraintName("FK__Ingredien__Aller__3B40CD36");
        });

        modelBuilder.Entity<KitchenTool>(entity =>
        {
            entity.HasKey(e => e.ToolId).HasName("PK__KitchenT__CC0CEBB12266E1BA");

            entity.HasIndex(e => e.Name, "UQ__KitchenT__737584F6BFD9949C").IsUnique();

            entity.Property(e => e.ToolId).HasColumnName("ToolID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipes__FDD988D0CD1CECE1");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Instructions).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipes__UserID__3493CFA7");

            entity.HasMany(d => d.Diets).WithMany(p => p.Recipes)
                .UsingEntity<Dictionary<string, object>>(
                    "RecipeDiet",
                    r => r.HasOne<Diet>().WithMany()
                        .HasForeignKey("DietId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RecipeDie__DietI__5224328E"),
                    l => l.HasOne<Recipe>().WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RecipeDie__Recip__51300E55"),
                    j =>
                    {
                        j.HasKey("RecipeId", "DietId").HasName("PK__RecipeDi__076DA7BB24B9D8E1");
                        j.ToTable("RecipeDiets");
                        j.IndexerProperty<int>("RecipeId").HasColumnName("RecipeID");
                        j.IndexerProperty<int>("DietId").HasColumnName("DietID");
                    });

            entity.HasMany(d => d.Tools).WithMany(p => p.Recipes)
                .UsingEntity<Dictionary<string, object>>(
                    "RecipeTool",
                    r => r.HasOne<KitchenTool>().WithMany()
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RecipeToo__ToolI__55F4C372"),
                    l => l.HasOne<Recipe>().WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RecipeToo__Recip__55009F39"),
                    j =>
                    {
                        j.HasKey("RecipeId", "ToolId").HasName("PK__RecipeTo__F119466B13BD6EBE");
                        j.ToTable("RecipeTools");
                        j.IndexerProperty<int>("RecipeId").HasColumnName("RecipeID");
                        j.IndexerProperty<int>("ToolId").HasColumnName("ToolID");
                    });
        });

        modelBuilder.Entity<RecipeComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__RecipeCo__C3B4DFAA86ECAAD6");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CommentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeComments)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RecipeCom__Recip__489AC854");

            entity.HasOne(d => d.User).WithMany(p => p.RecipeComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RecipeCom__UserI__498EEC8D");
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.IngredientId }).HasName("PK__RecipeIn__463363F775C2FCDB");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.Quantity)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RecipeIng__Ingre__3F115E1A");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RecipeIng__Recip__3E1D39E1");
        });

        modelBuilder.Entity<RecipeRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__RecipeRa__FCCDF85C5FE0DE0F");

            entity.Property(e => e.RatingId).HasColumnName("RatingID");
            entity.Property(e => e.RatingDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.Review).HasColumnType("text");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeRatings)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RecipeRat__Recip__43D61337");

            entity.HasOne(d => d.User).WithMany(p => p.RecipeRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RecipeRat__UserI__44CA3770");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC274802CD");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E43A8DBE87").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053403F025B4").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
