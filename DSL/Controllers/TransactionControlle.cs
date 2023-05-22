
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
public class TransactionController : ControllerBase
{

    private ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Transaction>>> GetAll([FromRoute] string id)
    {

        var transactions = await _service.GetAll(id);

        if (transactions is null)
        {
            return NoContent();
        }
        else
        {

            return transactions;
        };

    }

    [HttpGet]
    public async Task<ActionResult<Transaction>> Get(Guid id, string userId)
    {
        var transaction = await _service.GetTransaction(id, userId);

        if (transaction is null)
        {
            return NotFound();
        }
        else return transaction;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(TransactionDto transaction, string userId)
    {

        return Ok(await _service.Create(transaction, userId));
    }

}