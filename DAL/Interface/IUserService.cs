using Backend.DAL.Models;
using Backend.DAL.DTOs;
namespace Backend.DAL.Interface;

public interface IUserService
{

    public Task<User> Register(UserRegisterDto userDto);
    public Task<List<User>> GetUsers();
    public Task<User> GetUser(string id);
    public Task<User> Login(string email, string password);

}