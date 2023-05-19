namespace Backend.DAL.Models
{
    public class Budget
    {
        public int BudgetID{ get; set; }
        public float Amount{ get; set; }
        public int UserID{ get; set; }
        public DateTime Last_Update{ get; set; }
        public CategoriesEnum CategoryID{ get; set; }
        public virtual User? User {get;set;}
        public virtual Category? Category{get;set;}

    }
}