namespace PTS.Api.SystemTests;

using System.Text;
using System.Text.Json;

public class IdentifierControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        _client = new HttpClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    [Test]
    public async Task CreateAndGetIdentifier()
    {
        // Arrange
        var newIdentifier = new
        {
            Text = "TestIdentifier",
            HighestValue = 100,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(newIdentifier);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act - POST the identifier
        var postResponse = await _client.PostAsync("http://localhost:5021/identifier/createIdentifier", content);
        postResponse.EnsureSuccessStatusCode();

        // Act - GET all identifiers
        var getResponse = await _client.GetAsync("http://localhost:5021/identifier/getAllIdentifiers");
        getResponse.EnsureSuccessStatusCode();

        var responseBody = await getResponse.Content.ReadAsStringAsync();

        // Assert - basic check to see if the identifier appears in the response
        Console.WriteLine(responseBody);
        Assert.That(responseBody.Contains("TestIdentifier"), "Identifier was not found in the response.");
    }
}
