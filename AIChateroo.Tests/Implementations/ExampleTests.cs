using System.Globalization;
using System.Text.RegularExpressions;
using AIChateroo.Engines.Internet;
using AIChateroo.Engines.LLModels;
using LLama.Common;

namespace AIChateroo.Tests.Implementations;

public class ExampleOfFirstRunTests
{
    static int FindQuote(string s, int index)
    {
        return s.IndexOf('"', index);
    }
    public List<string> ExtractLinksBlabbermouth(string htmlContent)
    {
        var links = new List<string>();
        
        var pattern = @"/news/";
        var patternByIndex = @"https:\/\/blabbermouth.net\/news\/";
        
        
        MatchCollection matches = Regex.Matches(htmlContent, "@"+pattern);

        var ocurrencesIndex = htmlContent.IndexOf(patternByIndex);

        var quote1 = htmlContent[ocurrencesIndex - 1];

        if (quote1 == '"')
        {
            var value =FindQuote(htmlContent, ocurrencesIndex);
            // copy everything between to indexes
            var result = htmlContent.Substring(ocurrencesIndex, value - ocurrencesIndex);
            links.Add(result);
            
            // get everything after the index
            var rest = htmlContent.Substring(value);
            links.AddRange(ExtractLinksBlabbermouth(rest));
        }

        return links;
    }
    
    private string ReparseBadLink(string link)
    {
        
        var linkNew = link.Replace("\\", "/");
        linkNew = linkNew.Replace("//", "/");
        return linkNew;
    }
    
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
    
    public static string HtmlToStringConvertV2( string htmlString)
    {
        var htmlTagPattern = "<.*?>";
        var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)",
            RegexOptions.Singleline | RegexOptions.IgnoreCase);
        htmlString = regexCss.Replace(htmlString, string.Empty);
        htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
        htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
        htmlString = htmlString.Replace("&nbsp;", string.Empty);

        return htmlString;
    }
    
    
    // Write test method here
    [Test]
    public async Task Test1()
    {
        var url = "https://rockatuestilo.com";
        
        var result = await new RequestFromTheInternetChromeDriverRenderingV1().RunWebRequest(url);
        
        //string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/mistral-7b-instruct-v0.1.Q4_0.gguf"; // change it to your own model path
        string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/nous-hermes-llama2-13b.Q4_0.gguf"; // change it to your own model path
    
        
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 40069,
            Seed = 1337,
            GpuLayerCount = 5,
            
            
        };
        var textFromHtml = HtmlToStringConvertV2(result);
        
        //string textFromHtml = Regex.Replace(result, @"<[^>]*>", string.Empty);
        
        // Arrange
        ILlModelEngine illModelEngine = new LlModelEngine(parameters);
        var prompt = $"I need to to do an analisys of the website {url}";
        // add new line to string
        prompt += "\n";
        prompt += "here is the result of the rendered html source. I need to know if you can find the informative elements " +
                  "for a person";
        prompt += "\n";
        
        prompt += "I extracted the text from the HTML " +
                  "";
        prompt += "\n";
        prompt += textFromHtml;
        
        var session = illModelEngine.InitChatSession();
        var myTest = string.Empty;
        float temperature = 0.4f;
        await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), new InferenceParams { Temperature = temperature, AntiPrompts = [ "User:" ] }))
        {
            Console.Write(text);
            myTest+=text;
        }

        
        //var exampleOfFirstRun = new ExampleOfFirstRun();
        
        //var result = await exampleOfFirstRun.Run();
        
        Assert.Pass();
    }
    
    private string InstructionForChaGpt()
    {
        var preQuery =   "por favor necesito que leas este texto y " +
                         "lo reescribas en espanhol cambiando un poco el " +
                         "texto y que quede como un articulo de noticias comenzando con el titulo y luego el contenido " +
                         "si es posible" +
                         ": (recuerda que es información importante y " +
                         "si tienes información propia informativa sobre el articulo, la puedes agregar)" +
                         "Formatea la respuesta como en xml o json o yaml por ejemplo:" +
                         "<titulo></titulo>" +
                         "<contenido></contenido>" +
                         "";
        ;
        
        
        return preQuery;
    }
    
    private string InstructionForChaGpt2_1()
    {
        var preQuery = "traduce el texto a continuación al español y no me des lineas vacias sin sentido";
                         
        
        
        return preQuery;
    }
    
    static List<string> SplitIntoSentences(string text)
    {
        var sentences = new List<string>();
        var charInfo = new StringInfo(text);

        var enumerator = StringInfo.GetTextElementEnumerator(text);
        var sentenceStart = 0;
        var length = 0;

        while (enumerator.MoveNext())
        {
            var element = enumerator.GetTextElement();
            length += element.Length;

            // Check if the element is an ending punctuation mark (period, question mark, exclamation mark)
            if (element == "." || element == "?" || element == "!")
            {
                var sentence = charInfo.SubstringByTextElements(sentenceStart, length);
                sentences.Add(sentence.Trim());
                sentenceStart += length;
                length = 0;
            }
        }

        // Add the last sentence if there's any remaining part
        if (length > 0)
        {
            var lastSentence = charInfo.SubstringByTextElements(sentenceStart, length);
            sentences.Add(lastSentence.Trim());
        }

        return sentences;
    }
    
    [Test]
    public async Task Test2()
    {
        
        
        var url = "https://blabbermouth.net/";
        
        // Link to be searched
        var resultSearch = await RunWebRequest("https://blabbermouth.net/#");
        
        // Extract links
        var links = ExtractLinksBlabbermouth(resultSearch);
        
        links = links.Select(link=> ReparseBadLink(link)).ToList();

       
        
        //var result = await new RequestFromTheInternetChromeDriverRenderingV1().RunWebRequest(url);
        
        string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/mistral-7b-instruct-v0.1.Q4_0.gguf"; // change it to your own model path
        //string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/nous-hermes-llama2-13b.Q4_0.gguf"; // change it to your own model path

        
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 40069,
            Seed = 1337,
            GpuLayerCount = 32,
            
            
            
        };
        
        //var textFromHtml = HtmlToStringConvertV2(result);
        
        //string textFromHtml = Regex.Replace(result, @"<[^>]*>", string.Empty);
        
        // Arrange
        ILlModelEngine illModelEngine = new LlModelEngine(parameters);
        
        var session = illModelEngine.InitChatSession();
        var results = new List<string>();
        foreach (var link in links)
        {
            var newArticleRaw = await RunWebRequest(link);
            var newArticle = HtmlToStringConvertV2(newArticleRaw);

            var splits = SplitIntoSentences(newArticle);
            var splitsResults = new List<string>();
            foreach (var split in splits)
            {
                var prompt = InstructionForChaGpt2_1();
                prompt += Environment.NewLine;
                prompt += split;
                var myTest = string.Empty;
            
                float temperature = 0.6f;
            
                await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), new InferenceParams { Temperature = temperature, AntiPrompts = [ "User:" ] }))
                {
                    Console.Write(text);
                    myTest+=text;
                }
                splitsResults.Add(myTest);
            }
            

            //myTest = string.Empty;
            var joined = string.Join("\n",splitsResults.ToArray());
            results.Add(joined);
        }

        
        
       
        /*var prompt = $"I need to to do an analisys of the website {url}";
        // add new line to string
        prompt += "\n";
        prompt += "Esta es una página de noticias y necesito que me ayudes a encontrar los elementos informativos " +
                  "para crear noticias para mi página";
        prompt += "\n";
        
        prompt += "I extracted the text from the HTML " +
                  "";
        prompt += "\n";
        prompt += "textFromHtml";
        
        
        var myTest = string.Empty;
        float temperature = 0.4f;
        await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), new InferenceParams { Temperature = temperature, AntiPrompts = [ "User:" ] }))
        {
            Console.Write(text);
            myTest+=text;
        }

        
        //var exampleOfFirstRun = new ExampleOfFirstRun();
        
        //var result = await exampleOfFirstRun.Run();*/
        
        Assert.Pass();
    }
    
    
}