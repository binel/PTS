namespace PTS.Api.SystemTests;

public class IdentifierControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup() {
        _client = new HttpClient();
    }

    [TearDown]
    public void TearDown() {
        _client.Dispose();
    }

    [Test]
    public async Task GetAllIdentifiers_NoData() {
        // Replace with your actual endpoint
        var response = await _client.GetAsync("http://localhost:5021/identifier/test");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();

        // Example assertion: Check that response body contains a specific value
        Assert.That(body.Contains("test"), "Response did not contain expected value.");
    }
}
