using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Backend.DAL.Models;

public class User
{
    public User()
    {
        Budgets = new HashSet<Budget>();
        Transactions = new HashSet<Transaction>();
    }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public byte[]? Password { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public int Capital { get; set; }
    public Status Status { get; set; }
    // [JsonIgnore]
    // [JsonIgnore]
    public virtual ICollection<Budget> Budgets { get; set; }
    // [JsonIgnore]
    public virtual ICollection<Transaction> Transactions { get; set; }
}


