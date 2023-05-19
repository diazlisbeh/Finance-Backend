using System.Collections;
using Backend.DAL.Models;

namespace Backend.DAL.Interface;

public interface IBudgetService
{
    Task<IEnumerable> GetAll (int id);
    
    


}