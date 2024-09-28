namespace AIChateroo.Engines.Search.Configuration;

public interface ISearchService
{
    Task<string> SearchAsync(string query);
}

public class DuckDuckGoSearcherService: ISearchService
{
    public async Task<string> RunWebRequest(string url)
    {
        //string query = "most recent winner of the World Series";
        //string googleSearchUrl = $"https://www.google.com/search?q={Uri.EscapeDataString(url)}";

        using var client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string responseContent = await response.Content.ReadAsStringAsync();

            // Output the response content to the console (this will be the raw HTML of the search results page)
            //Console.WriteLine(responseContent);
            //return string.Join("\n",ExtractLinks(responseContent).ToArray());
            return responseContent;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }

        return null;
    }
    
    
    public async Task<string> SearchAsync(string query)
    {
        // Initialize ChromeDriver
        /*var driver = new ChromeDriver();

        // Open DuckDuckGo website
        await driver.NavigateToAsync("https://duckduckgo.com/");

        // Enter search query
        var searchBar = driver.FindElement(By.Name("q"));
        await searchBar.SendKeysAsync(query);
        await searchBar.SubmitAsync();

        // Wait for search results to load
        await Task.Delay(TimeSpan.FromSeconds(5));

        // Get the HTML content of the search results page
        var html = await driver.PageSourceAsync();

        // Close ChromeDriver
        await driver.QuitAsync();

        return html;*)*/
        
        return String.Empty;

    }
}