using MiApiWeb.Models;
using MiApiWeb.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<HuespedDBSettings>(builder.Configuration.GetSection("HuespedDBSettings"));

builder.Services.AddSingleton<HuespedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/huesped", async (HuespedService HuespedService) => {
    var huespedes = await HuespedService.GetAsync();
    return huespedes;
});
app.MapGet("/huesped/{id}", async (HuespedService HuespedService, string id) => {
    var Huesped = await HuespedService.GetAsync(id);
    return Huesped is null ? Results.NotFound() : Results.Ok(Huesped);
});
app.MapPost("/huesped", async (HuespedService HuespedService, huesped huesped) => {
    await HuespedService.CreateAsync(huesped);
    return huesped;
});
app.MapPut("/huesped/{id}", async (HuespedService HuespedService, string id, huesped huesped) => {
    await HuespedService.UpdateAsync(id, huesped);
    return huesped;
});
app.MapDelete("/huesped/{id}", async (HuespedService HuespedService, string id) => {
    await HuespedService.RemoveAsync(id);
    return Results.Ok();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
