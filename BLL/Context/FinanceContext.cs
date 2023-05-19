using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Backend.DAL.Models;
using Backend.BLL.Data;

namespace Backend.BLL.Context
{
    public partial class FinanceContext : DbContext
    {
        private IConfiguration _config;
        public DbSet<User> users {get;set;}
        public DbSet<Category> Categories{get;set;}
        
        public DbSet<Budget> Budgets{get;set;}
        public DbSet<Transaction> Transactions {get;set;}
        public FinanceContext(DbContextOptions<FinanceContext> options, IConfiguration config) :base(options){
            _config = config;
        }
      
        protected override void OnModelCreating(ModelBuilder model){
            
            model.ApplyConfiguration(new UserConfiguration() );
            model.ApplyConfiguration(new CategoryConfiguration() );
            model.ApplyConfiguration(new BudgetConfiguration() );
            model.ApplyConfiguration(new TransactionConfiguration() );

            OnModelCreatingPartial(model);
        }
        partial void OnModelCreatingPartial(ModelBuilder model);
        
    }
}