using Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.BLL.Data;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("budget");

        builder.Property(p => p.BudgetID).HasColumnName("budgetId");
        builder.HasKey(p => p.BudgetID);

        builder.Property(p => p.Amount).HasColumnName("amount");
        builder.Property(p => p.Last_Update).HasColumnName("last_update");
        builder.Property(p => p.CategoryID).HasColumnName("categoryID");
        builder.Property(p => p.UserID).HasColumnName("userId");

        builder.HasOne(p => p.Category)
        .WithMany(p => p.Budgets)
        .HasForeignKey(p => p.CategoryID)
        .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(p => p.User)
        .WithMany(p => p.Budgets)
        .HasForeignKey(p => p.UserID)
        .OnDelete(DeleteBehavior.NoAction)
        .HasConstraintName("Fk_budget_userid");

    }
}