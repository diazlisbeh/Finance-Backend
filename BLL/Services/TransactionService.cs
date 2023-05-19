using Backend.DAL.Interface;
using Backend.DAL.Models;
using Backend.BLL.Context;
using System.Collections;
using Backend.DAL.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Backend.BLL.Services;

public class TransactionService : ITransactionService
{
    private FinanceContext _context;
    private IMapper _mapper;
    public TransactionService (FinanceContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }
    public List<Transaction> GetAll(int Id)
    {
         
        return  _context.Transactions.Where(p => p.UserID == Id).ToList();
    }

    public Transaction GetTransaction(Guid id)
    {
        return _context.Transactions.FirstOrDefault(p => p.TransactionID == id);
    }

    public async Task<TransactionDto> Create(TransactionDto transactionDto){
        var transaction = new Transaction(){
            TransactionID = transactionDto.TransactionID,
            Amount = transactionDto.Amount,
            CategoryID = transactionDto.CategoryId,
            UserID = transactionDto.UserID,
            Date = DateTime.Parse(transactionDto.Date),
            Porpuse = transactionDto.Porpuse,
            Type = transactionDto.Type
        };
        var user = await _context.users.SingleOrDefaultAsync(u => u.Id == transactionDto.UserID); 
        
        if(transactionDto.Type ==0){
            user.Capital = user.Capital + (decimal)transactionDto.Amount;

        }else{
            user.Capital = user.Capital - (decimal)transactionDto.Amount;

        }
        _context.Update(user);
        await _context.Transactions.AddAsync(transaction);
        
        await _context.SaveChangesAsync();
        return transactionDto;

    }
}