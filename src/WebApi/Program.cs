using Infrastructure.Data;
using Domain.Interfaces;
using Application.UseCases;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddPolicy("bad", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<CreateOrderUseCase>();

var app = builder.Build();

app.UseCors("bad");

   
app.Use(async (ctx, next) =>
{
    try     
    {
        await next();
    }
    catch (Exception)
    {
        ctx.Response.StatusCode = 500;
        await ctx.Response.WriteAsync("Ocurrió un error interno en el servidor.");
    }
});

app.MapGet("/health", () => "ok");

app.MapControllers();

app.Run();