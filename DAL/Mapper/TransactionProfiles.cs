using AutoMapper;
using Backend.DAL.DTOs;
using Backend.DAL.Models;

namespace Backend.DAL.Mapper;

public class TransactionProfile : Profile{
    public TransactionProfile(){
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionDto, Transaction>();
    }
}