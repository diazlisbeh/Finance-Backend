
using System.Collections;
using Backend.DAL.DTOs;
using Backend.DAL.Models;

namespace Backend.DAL.Interface;

public interface ITransactionService
{

    Task<Transaction> GetTransaction(Guid id, string userId);
    Task<List<Transaction>> GetAll(string userId);

    Task<TransactionDto> Create(TransactionDto transaction, string userId);
}