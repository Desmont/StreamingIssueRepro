using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));

// Configure the HTTP request pipeline.

app.MapGet("/weatherforecast", (CancellationToken cancellationToken) => ReturnAsyncEnumerable(cancellationToken));

app.Run();

async IAsyncEnumerable<int> ReturnAsyncEnumerable([EnumeratorCancellation] CancellationToken cancellationToken)
{
    throw new NotImplementedException();

    yield return 1;
    yield return 2;
}

public partial class Program;
