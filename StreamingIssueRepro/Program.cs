using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddSingleton<IStreamRequestHandler<ForecastRequest, WeatherForecast>, ForecastRequestHandler>();

var app = builder.Build();
app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));

// Configure the HTTP request pipeline.


app.MapGet("/weatherforecast", ([FromServices]IMediator mediator) =>
{
    return mediator.CreateStream(new ForecastRequest());
});
app.MapGet("/weatherforecast-direct", ([FromServices] IStreamRequestHandler<ForecastRequest, WeatherForecast> handler) =>
{
    return handler.Handle(new ForecastRequest(), default);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

class ForecastRequest : IStreamRequest<WeatherForecast>;

class ForecastRequestHandler : IStreamRequestHandler<ForecastRequest, WeatherForecast>
{
    public IAsyncEnumerable<WeatherForecast> Handle(ForecastRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

        return forecast.ToAsyncEnumerable();
    }
}
