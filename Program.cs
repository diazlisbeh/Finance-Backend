using Backend.DSL.Filter;
using Backend.DAL.Models;
using Backend.DAL.Interface;
using Backend.BLL.Services;
using Backend.BLL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("local");
var policy = "WebPolicy";
//Dependy Injection to Database


builder.Services.AddDbContext<FinanceContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//Auto Mapper
builder.Services.AddAutoMapper(typeof(Program));

//Adding Filters
builder.Services.AddMvc(options =>
{
    options.Filters.Add<JsonExceptionFilter>();
    options.Filters.Add<RequiredHttpsOrCloseAttribute>();
});

//Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)
        ),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy, policy =>
    {
        // policy.WithOrigins("http://localhost:3000/");
        policy.AllowAnyOrigin().
        // WithMethods("GET","POST").
        AllowAnyMethod().
        AllowAnyHeader();
    });
});

//Add Scopes
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    IdentityModelEventSource.ShowPII = true;
}

app.UseHttpsRedirection();

app.UseCors(policy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
