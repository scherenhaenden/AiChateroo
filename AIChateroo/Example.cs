using AIChateroo.Engines.LLModels;
using LLama;
using LLama.Common;

namespace AIChateroo;

public class ExampleOfFirstRun
{
    public async Task<string> Run()
    {
        
        string modelPath = "/Users/edwardflores/Library/Application Support/nomic.ai/GPT4All/mistral-7b-instruct-v0.1.Q4_0.gguf"; // change it to your own model path
        var prompt = "Transcript of a dialog, where the User interacts with an Assistant named Bob. Bob is helpful, kind, honest, good at writing, and never fails to answer the User's requests immediately and with precision.\r\n\r\nUser: Hello, Bob.\r\nBob: Hello. How may I help you today?\r\nUser: Please tell me the largest city in Europe.\r\nBob: Sure. The largest city in Europe is Moscow, the capital of Russia.\r\nUser:"; // use the "chat-with-bob" prompt here.

// Load a model
        var parameters = new ModelParams(modelPath)
        {
            ContextSize = 40069,
            Seed = 1337,
            GpuLayerCount = 32
        };
        
        ILlModelEngine illModelEngine = new LlModelEngine(parameters);
        ChatSession session = illModelEngine.InitChatSession();

// show the prompt
        Console.WriteLine();
        Console.Write(prompt);
        
        

// run the inference in a loop to chat with LLM

var myTest = string.Empty;


        while (prompt != "stop")
        {
            await foreach (var text in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, prompt), new InferenceParams { Temperature = 0.6f, AntiPrompts = [ "User:" ] }))
            {
                Console.Write(text);
                myTest+=text;
            }
            prompt = Console.ReadLine() ?? "";
        }

// save the session
        session.SaveSession("SavedSessionPath");

        return myTest;
    }
}