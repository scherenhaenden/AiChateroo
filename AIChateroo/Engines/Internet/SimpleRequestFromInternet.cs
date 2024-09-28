namespace AIChateroo.Engines.Internet;

public class SimpleRequestFromInternet:IRequestFromInternet
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
}