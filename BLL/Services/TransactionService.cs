using Backend.DAL.Interface;
using Backend.DAL.Models;
using Backend.BLL.Context;
using System.Collections;
using Backend.DAL.DTOs;
using AutoMapper;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
// using Microsoft.EntityFrameworkCore;

namespace Backend.BLL.Services;

public class TransactionService : ITransactionService
{
    private IMongoCollection<User> _collection;

    public TransactionService(IOptions<Settings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionStrings);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<User>("users");

    }
    public async Task<List<Transaction>> GetAll(string userId)
    {
        var t = await _collection.Find(x => x.Id == userId).FirstOrDefaultAsync();
        return t.Transactions.ToList();

    }

    public async Task<Transaction> GetTransaction(Guid id, string userId)
    {
        var user = await _collection.Find(x => x.Id == userId).FirstOrDefaultAsync();
        if (user is null) return null;

        var transaction = user.Transactions.Where(x => x.TransactionID == id).FirstOrDefault();
        if (transaction is null) return null;

        return transaction;
    }

    public async Task<TransactionDto> Create(TransactionDto transactionDto, string userId)
    {
        var transaction = new Transaction()
        {
            TransactionID = transactionDto.TransactionID,
            Amount = transactionDto.Amount,
            CategoryID = transactionDto.CategoryId,
            CategoryName = transactionDto.CategoryName,
            Date = DateTime.Parse(transactionDto.Date),
            Porpuse = transactionDto.Porpuse,
            Type = transactionDto.Type
        };
        var user = await _collection.Find(x => x.Id == userId).FirstOrDefaultAsync();

        if (transactionDto.Type == 0)
        {
            _collection.UpdateOne(u => u.Id == userId, Builders<User>.Update.Set(x => x.Capital, transactionDto.Amount + user.Capital));
        }
        else
        {
            _collection.UpdateOne(u => u.Id == userId, Builders<User>.Update.Set(x => x.Capital, user.Capital - transactionDto.Amount));
        }

        _collection.UpdateOne(u => u.Id == userId, Builders<User>.Update.Push(u => u.Transactions, transaction));
        return transactionDto;

    }
}