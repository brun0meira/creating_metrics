using Microsoft.AspNetCore.Mvc;

using Injection;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<HatCoMetrics>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/complete-sale", ([FromBody] SaleModel model, HatCoMetrics metrics) =>
    {
        // ... business logic such as saving the sale to a database ...

        metrics.HatsSold(model.QuantitySold);
    }).WithName("CompleteSale")
    .WithOpenApi();;

app.Run();

public class SaleModel
{
    public int QuantitySold { get; set; }
}