using Backend.DAL.Models;
using Backend.DAL.DTOs;
namespace Backend.DAL.Interface;

public interface IUserService{

    public Task<User> Register(UserRegisterDto userDto);

    // public List<User> GetUsers();
    List<User> GetUsers();
    Task<User>  GetUser(int id);
    Task<User> Login (string email, string password);

}