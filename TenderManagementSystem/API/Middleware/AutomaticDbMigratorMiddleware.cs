using Microsoft.EntityFrameworkCore;
using TenderManagementSystem.Infrastructure.Data;

namespace TenderManagementSystem.API.Middleware;

public class AutomaticDbMigratorMiddleware
{
    private readonly RequestDelegate _next;

    public AutomaticDbMigratorMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var db = httpContext.RequestServices.GetRequiredService<AppDbContext>();
        if (db.Database.IsRelational())
        {
            await db.Database.MigrateAsync(httpContext.RequestAborted);
        }

        await _next(httpContext);
    }
}