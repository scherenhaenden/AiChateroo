using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AIChateroo.Engines.Internet;

public class RequestFromTheInternetChromeDriverRenderingV1:IRequestFromInternet
{
    public async Task<string> RunWebRequest(string url)
    {
        
        // Specify the path to chromedriver
        //var chromeDriverPath = "/usr/local/bin/chromedriver"; // Update the path if needed
        
        // get current directory
        var currentDirectory =  Directory.GetCurrentDirectory() + "/../../../../";
        
        // Specify the path to chromedriver using currentDirectory
        
        
        
        
        var chromeDriverPath = Path.Combine(currentDirectory, "chromedriver"); // Update the path if needed
        
        //check if file exists
        if (!File.Exists(chromeDriverPath))
        {
            Console.WriteLine("ChromeDriver not found");
            
        }
        
        

        // Set the path to chromedriver executable
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("start-maximized"); // Optional: maximize the browser window
        
        /*using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        BrowserTypeLaunchOptions options = new BrowserTypeLaunchOptions();
        options.Headless = true;
        await using var browser = await playwright.Chromium.LaunchAsync(options);
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://example.com");*/
        
        
        
        FirefoxOptions firefoxOptions = new FirefoxOptions();
        firefoxOptions.AddArguments("--headless");
        FirefoxDriver driverFr = new FirefoxDriver(firefoxOptions);
        driverFr.Navigate().GoToUrl(url);
        
        WebDriverWait wait = new WebDriverWait(driverFr, TimeSpan.FromSeconds(10));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        
        string renderedHtml = driverFr.PageSource;
        
        driverFr.Quit();
        return renderedHtml;
   

        // Initialize ChromeDriver with the specified path
        using (var driver = new ChromeDriver(chromeDriverPath, chromeOptions))
        {
            // Your Selenium code here
            driver.Navigate().GoToUrl(url);

            // Example: Find an element by name and click it
            //var element = driver.FindElement( By.Name("q"));
            //element.SendKeys("Selenium");
            //element.Submit();
        }
        
       
        
        
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