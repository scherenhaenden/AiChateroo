using LLama;
using LLama.Common;
using LLamaSharp.KernelMemory;
using Microsoft.KernelMemory;
using Microsoft.KernelMemory.Configuration;

namespace AIChateroo.Engines.LLModels;

public class LlModelEngine : ILlModelEngine
{
    private readonly ModelParams _parameters;

    private LLamaWeights? model;

    public LlModelEngine( ModelParams parameters)
    {
        _parameters = parameters;
    }

    private void LoadModel()
    {
        if (model is null)
        {
            model = LLamaWeights.LoadFromFile(_parameters);
        }
        
    }
    
    
    public ChatSession? InitChatSession()
    {
        LoadModel();

        try
        { 
            var context = model!.CreateContext(_parameters);
        
            var ex = new InteractiveExecutor(context);
            ChatSession session = new ChatSession(ex);
        
            return session;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
       
    }

    public IKernelMemory? Build()
    {
       var lLamaSharpConfig =  new LLamaSharpConfig(_parameters.ModelPath);

       InferenceParams inferenceParams = new InferenceParams();
       inferenceParams.AntiPrompts = new List<string> { "\n\n" };

       lLamaSharpConfig.DefaultInferenceParams = inferenceParams;
       
       var searchClientConfig = new SearchClientConfig
       {
           MaxMatchesCount = 1,
           AnswerTokens = 100,
       };
       

        var memory = new KernelMemoryBuilder()
            
            .WithLLamaSharpDefaults(lLamaSharpConfig)
            .WithSearchClientConfig(searchClientConfig)
            .With(new TextPartitioningOptions
            {
                MaxTokensPerParagraph = 300,
                MaxTokensPerLine = 100,
                OverlappingTokens = 30
            })
            .Build();

        return memory;
    }
}