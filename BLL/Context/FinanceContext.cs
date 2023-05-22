using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Backend.DAL.Models;
using Backend.BLL.Data;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Backend.BLL.Context;

public class FinanceContext
{

    private readonly IMongoDatabase? _database;

    public FinanceContext(IOptions<Settings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionStrings);


        _database = client.GetDatabase(settings.Value.DatabaseName);


    }
}
