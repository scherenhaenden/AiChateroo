using AIChateroo.Engines.LLModels;
using LLama.Common;

namespace AIChateroo.Tests.Implementations.Engines.LLModels;

public class LlModelEngineTests
{
    
    
    // Write test method empty
    [Test]
    public async Task Test1()
    {
        string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/mistral-7b-instruct-v0.1.Q4_0.gguf"; // change it to your own model path
    
        
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 40069,
            Seed = 1337,
            GpuLayerCount = 5,
            
            
        };
        
        
        
        
        // Arrange
        ILlModelEngine illModelEngine = new LlModelEngine(parameters);
        var prompt = "give yourself a name that is exactly 14 characters long";
        // Act
        
        var session = illModelEngine.InitChatSession();
        
        // Assert
        
        
        Console.WriteLine();
        Console.Write(prompt);
        // Assert
        
        var myTest = string.Empty;
        
        await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), new InferenceParams { Temperature = 0.4f, AntiPrompts = [ "User:" ] }))
        {
            Console.Write(text);
            myTest+=text;
        }
        prompt = Console.ReadLine() ?? "";
        
        Assert.That(prompt, Is.Not.Null);
        Assert.That(prompt.Length, Is.GreaterThan(13));

    }
    
    
    
    [Test]
    public async Task Test2()
    {
        string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/mistral-7b-instruct-v0.1.Q4_0.gguf"; // change it to your own model path
    
        
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 40069,
            Seed = 1337,
            GpuLayerCount = 32,
            
            
        };
        
        
        
        
        // Arrange
        ILlModelEngine illModelEngine = new LlModelEngine(parameters);
        var prompt = "give yourself a name that is exactly 14 characters long";
        var originalPrompt = prompt;
        // Act
        
        var session = illModelEngine.InitChatSession();
        
        // Assert
        
        
        Console.WriteLine();
        Console.Write(prompt);
        // Assert
        
        var myTest = string.Empty;
        List<string> prompts = new List<string>();
        
        float temperature = 0.1f;
        
        // right lop for 9 numbers
        for (int i = 0; i < 9; i++)
        {
            
            await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), new InferenceParams { Temperature = temperature, AntiPrompts = [ "User:" ] }))
            {
                Console.Write(text);
                myTest+=text;
            }
            //prompt = Console.ReadLine() ?? "";
            prompts.Add(myTest);
            myTest = string.Empty;
            temperature += 0.1f;
            prompt = originalPrompt;
           
        }
        Assert.That(prompt, Is.Not.Null);
        Assert.That(prompt.Length, Is.GreaterThan(13));
   
    }
    
    
    [Test]
    public async Task Test3()
    {
        string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/Meta-Llama-3-8B-Instruct.Q4_0.gguf"; // change it to your own model path
    
        
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 40069,
            Seed = 1337,
            GpuLayerCount = 32,
            
            
        };
        
        // Arrange
        ILlModelEngine illModelEngine = new LlModelEngine(parameters);
        var prompt = "Now you are the ";
        var originalPrompt = prompt;
        // Act
        
        var session = illModelEngine.InitChatSession();
        
        // Assert
        
        
        Console.WriteLine();
        Console.Write(prompt);
        // Assert
        
        var myTest = string.Empty;
        List<string> userNames = new List<string>();
        userNames.Add("Project Manager");
        userNames.Add("Software Engineer");
        userNames.Add("Data Scientist");
        userNames.Add("Marketing Manager");
        
       
        List<string> prompts = new List<string>();
        
        prompt = "Now you are the ";
        float temperature = 0.7f;
        while (true)
        {
            foreach (var user in userNames)
            {
                
                prompt = "Now you are the " + user + " introduce yourself";
                
                await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), 
                                   new InferenceParams { 
                                       Temperature = temperature, 
                                       AntiPrompts = [ "User:" ] })
                              )
                {
                    Console.Write(text);
                    myTest+=text;
                }
                //prompt = Console.ReadLine() ?? "";
                prompts.Add(myTest);
                myTest = string.Empty;
                //temperature += 0.6f;
               
            }
        }
        
        
       
        
        // right lop for 9 numbers
        for (int i = 0; i < 9; i++)
        {
            
            await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), 
                               new InferenceParams { 
                                   Temperature = temperature, 
                                   AntiPrompts = [ "User:" ] })
                           )
            {
                Console.Write(text);
                myTest+=text;
            }
            //prompt = Console.ReadLine() ?? "";
            prompts.Add(myTest);
            myTest = string.Empty;
            temperature += 0.6f;
            prompt = originalPrompt;
           
        }
        Assert.That(prompt, Is.Not.Null);
        Assert.That(prompt.Length, Is.GreaterThan(13));
   
    }

   
}