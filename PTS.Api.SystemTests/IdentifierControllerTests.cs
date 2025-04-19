namespace PTS.Api.SystemTests;

using System.Net.Http.Headers;
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
            Text = "TestIdentifier"
        };

        var json = JsonSerializer.Serialize(newIdentifier);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var username = "Test";
        var password = "autotess";
        var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

        // create a new identifier 
        var postResponse = await _client.PostAsync("http://localhost:5021/identifier/createIdentifier", content);
        postResponse.EnsureSuccessStatusCode();

        // verify the identifier was added 
        var getResponse = await _client.GetAsync("http://localhost:5021/identifier/getAllIdentifiers");
        getResponse.EnsureSuccessStatusCode();

        var responseBody = await getResponse.Content.ReadAsStringAsync();

        Console.WriteLine(responseBody);
        Assert.That(responseBody.Contains("TestIdentifier"), "Identifier was not found in the response.");
    }
}
