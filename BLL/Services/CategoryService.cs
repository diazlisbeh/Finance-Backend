using System.Collections;
using Backend.BLL.Context;
using Backend.DAL.Interface;
using Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private FinanceContext _context;

    public CategoryService(FinanceContext context){
        _context = context;
    }

    public async Task<IEnumerable<Category>> Get()
    {
        return await _context.Categories.ToListAsync();
    }
}