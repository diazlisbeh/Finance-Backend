using Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Backend.DSL.Filter;

public class JsonExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _env;
    public JsonExceptionFilter (IWebHostEnvironment env){
        _env = env;
    }


    public void OnException(ExceptionContext context)
    {
        var error = new Error();

        if(_env.IsDevelopment()){
            error.Message = context.Exception.Message;
            error.Details = context.Exception.StackTrace;
        }
        else{
            error.Message = "An error was ocurred";
            error.Details = context.Exception.Message;
        }
    }
}