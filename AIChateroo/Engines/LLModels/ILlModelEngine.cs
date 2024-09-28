using LLama;
using Microsoft.KernelMemory;

namespace AIChateroo.Engines.LLModels;

public interface ILlModelEngine
{
    // Chat Session
    public ChatSession? InitChatSession();
    
    // Semantic Kernel

    public IKernelMemory? Build();

}