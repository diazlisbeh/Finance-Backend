using System.Collections;
using Backend.DAL.Models;

namespace Backend.DAL.Interface;

public interface ICategoryService{

    public Task<IEnumerable<Category>> Get();
}