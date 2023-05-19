using Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.BLL.Data;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transaction");

        builder.Property(p => p.TransactionID).HasColumnName("transactionId");
        builder.HasKey(p => p.TransactionID);

        builder.Property(p => p.Amount).HasColumnName("amount");
        builder.Property(p => p.Date).HasColumnName("date");
        builder.Property(p => p.CategoryID).HasColumnName("categoryID");
        builder.Property(p => p.UserID).HasColumnName("userId");
        builder.Property(p => p.Porpuse).HasColumnName("porpuse");

        builder.HasOne(p => p.Category)
        .WithMany(p => p.Movements)
        .HasForeignKey(p => p.CategoryID)
        .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(p => p.User)
        .WithMany(p => p.Transactions)
        .HasForeignKey(p => p.UserID)
        .OnDelete(DeleteBehavior.NoAction);

    }
}