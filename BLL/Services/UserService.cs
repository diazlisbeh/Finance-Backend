using System.Security.Cryptography;
using Backend.DAL.Models;
using Backend.DAL.Interface;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.BLL.Context;
using Backend.DAL.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Backend.BLL.Services;


public class UserService : IUserService
{
    private IMongoCollection<User> _collection;
    public UserService(IOptions<Settings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionStrings);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<User>("users");
    }

    public async Task<List<User>> GetUsers()
    {
        // List<User> users = await _collection.Find(_ => true).ToListAsync();
        return await _collection.Find(_ => true).ToListAsync();
        // return users;
    }
    public async Task<User> GetUser(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> Register(UserRegisterDto userDto)
    {
        var exist = await _collection.Find(x => x.Email == userDto.Email).FirstOrDefaultAsync();

        if (exist is not null)
        {
            return null;
        }

        CreatePassword(userDto.password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User()
        {
            Name = userDto.Name,
            LastName = userDto.LastName,
            Email = userDto.Email.ToLower(),
            Password = passwordHash,
            PasswordSalt = passwordSalt,
            Capital = 0

        };

        await _collection.InsertOneAsync(user);
        return user;
    }


    public async Task<User> Login(string email, string password)
    {

        var user = await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
        if (user is null)
        { return null; }
        if (!VerifyPassword(password, user.PasswordSalt, user.Password)) { return null; }
        return user;
    }


    private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }


    private bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {

            var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {

                if (ComputeHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

}