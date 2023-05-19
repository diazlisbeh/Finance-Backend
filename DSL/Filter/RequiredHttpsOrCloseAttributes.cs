using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;


namespace Backend.DSL.Filter;

public class RequiredHttpsOrCloseAttribute : RequireHttpsAttribute
{
    protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
    {
        filterContext.Result = new StatusCodeResult(400);
    }
}