using Microsoft.AspNetCore.Mvc.Testing;

namespace StreamingIssueReproTests;

public class StreamingResponseTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public StreamingResponseTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task ReproduceIssue()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");
        
        // Assert
        var content = await response.Content.ReadAsStringAsync();
        
        // Should start with a single JSON object representing problem details response, not a symbol representing an array.
        Assert.StartsWith("{", content);
    }
}