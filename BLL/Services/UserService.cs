using System.Security.Cryptography;
using Backend.DAL.Models;
using Backend.DAL.Interface;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.BLL.Context;
using Backend.DAL.DTOs;

namespace Backend.BLL.Services;


public class UserService : IUserService
{
    private FinanceContext _context;
    public UserService(FinanceContext context){
        _context = context;
    }

    public  List<User> GetUsers()
    {   
        List<User> user =  _context.users.ToList();
        return user;
    }
    public async  Task<User> GetUser(int id)
    {   
        return  await _context.users.SingleOrDefaultAsync  (x => x.Id == id);
        
    }

    public async Task<User>  Register(UserRegisterDto userDto)
    {
        var exist = _context.users.Any(p => p.Email == userDto.Email);

        if(exist){
            return null;
        }

        CreatePassword(userDto.password,out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User(){
            Name= userDto.Name,
            LastName = userDto.LastName,
            Email = userDto.Email.ToLower(),
            Password = passwordHash,
            PasswordSalt = passwordSalt,
            Capital = 0
            
        };

        await _context.users.AddAsync(user);
        await _context.SaveChangesAsync();
        return  user;
    }


    public async Task<User> Login (string email, string password){

        var user = await _context.users.FirstOrDefaultAsync(usr => usr.Email == email);
        if(user is null) 
        {return null;}
        if(!VerifyPassword(password,user.PasswordSalt,user.Password)){ return null;}
        return user;
    }


    private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt){
        using(var hmac = new HMACSHA512()){
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }


    private bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash){
        using(var hmac = new HMACSHA512(passwordSalt)){
          
          var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

          for(int i=0; i<ComputeHash.Length;i++){
            
            if(ComputeHash[i] != passwordHash[i]){
                return false;
            }
          } 
          return true; 
        }
    }

}