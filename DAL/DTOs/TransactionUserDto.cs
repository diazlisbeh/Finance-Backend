using Backend.DAL.Models;

namespace Backend.DAL.DTOs;


public class TransactionUserDto{

    public Guid TransactionID {get;set;}
    public float Amount{get;set;}
    public string CategoryId{get;set;}
    public int UserID{ get; set; }
    public string Date { get; set; }
    public string? Porpuse{ get; set; }
    public string Type {get;set;}

}