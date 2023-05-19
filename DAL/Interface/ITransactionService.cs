
using System.Collections;
using Backend.DAL.DTOs;
using Backend.DAL.Models;

namespace Backend.DAL.Interface;

public interface ITransactionService{

    Transaction GetTransaction(Guid id);
    public List<Transaction> GetAll(int Id);

    Task<TransactionDto> Create(TransactionDto transaction);
}