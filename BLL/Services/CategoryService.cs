using System.Collections;
using Backend.BLL.Context;
using Backend.DAL.Interface;
using Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class CategoryService : ICategoryService
{
    private IMongoCollection<Category> _collection;

    public CategoryService(IOptions<Settings> setting)
    {
        var client = new MongoClient(setting.Value.ConnectionStrings);
        var database = client.GetDatabase(setting.Value.DatabaseName);
        _collection = database.GetCollection<Category>("Categories");
    }

    // public async Task<IEnumerable<Category>> Get()
    // {
    // return await _context.Categories.ToListAsync();
    // }

    public async Task<List<Category>> Get()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}