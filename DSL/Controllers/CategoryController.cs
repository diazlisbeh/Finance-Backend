
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
public class CategoryController : ControllerBase{

    private ICategoryService _service; 

    public CategoryController(ICategoryService service){
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(){
        // var categories = await _service.Get();
        return Ok(await _service.Get());
    }

}