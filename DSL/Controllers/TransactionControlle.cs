
using Backend.DAL.Interface;
using Backend.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.DAL.Models;
using Backend.DAL.DTOs;
using Microsoft.AspNetCore.Cors;

namespace Backend.DSL.Controller;

[ApiController]
[Route("[controller]")]
[EnableCors("WebPolicy")]
public class TransactionController : ControllerBase{

    private ITransactionService _service; 

    public TransactionController(ITransactionService service){
        _service = service;
    }

    [HttpGet("{id}")]
    public IActionResult GetAll([FromRoute] int id){

        var transactions = _service.GetAll(id);

        if(transactions is null ){
            return NoContent();
        }else {
            
            return Ok(transactions);
        };

    }

    [HttpGet]
    public IActionResult Get( Guid id){
        var transaction = _service.GetTransaction(id);

        if(transaction is null ){
            return NotFound();
        }else return Ok(transaction);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(TransactionDto transaction){
         
        return Ok( await _service.Create(transaction)); 
    }

}