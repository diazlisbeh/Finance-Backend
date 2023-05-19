namespace Backend.DAL.Models
{
    public class Category {
        public CategoriesEnum Id{ get; set; }
        public string? Name{ get; set; } 
        public string? Description {get;set;}
        public virtual ICollection<Budget>? Budgets{ get; set; }
        public virtual ICollection<Transaction>? Movements {get;set;}
    }
}


