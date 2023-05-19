using Backend.DAL.Models;

namespace Backend.DAL.DTOs;


public class TransactionDto{

    public Guid TransactionID {get;set;}
    public float Amount{get;set;}
    public CategoriesEnum CategoryId{get;set;}
    public int UserID{ get; set; }
    public string Date { get; set; }
    public string? Porpuse{ get; set; }
    public TransactionType Type {get;set;}

}