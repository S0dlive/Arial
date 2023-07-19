namespace CourseService.Services;

public class IntrospectionService
{
    private readonly HttpClient _httpClient;
    public IntrospectionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetIdOfTheTokenByIntrospect(string token)
    {
        string introspectionEndpoint = "https://localhost:7262/introspect";
        var requestContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("token", token),
        });
        
        var response = await _httpClient.PostAsync(introspectionEndpoint, requestContent);
        if (response.IsSuccessStatusCode)
        {
            throw new NotImplementedException();
        }
        else
        {
            return "THE REQUEST IS DOWN lol";
        }
    }
}