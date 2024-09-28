namespace AIChateroo.Modelling.Models;

public class ConfigurationParametersModel
{
    
    public uint? ContextSize { get; set; }
    public int MainGpu { get; set; }
    public int GpuLayerCount { get; set; }
    public uint Seed { get; set; }
    public bool UseMemorymap { get; set; }
    public bool UseMemoryLock { get; set; }
    public string ModelPath { get; set; }

    public List<string> LoraAdapters { get; set; }
    public uint LoraBase { get; set; }
    public int Threads { get; set; }
    public int? BatchThreads { get; set; }
    public int? BatchSize { get; set; }
    public string EmbeddingMode { get; set; }
    public Dictionary<string, int> TensorSplits { get; set; }
    public Dictionary<string, object> MetadataOverrides { get; set; }
    public uint RopeFrequencyBase { get; set; }
    public double RopeFrequencyScale { get; set; }
    public double YarnExtrapolationFactor { get; set; }
    public double YarnAttentionFactor { get; set; }
    public double YarnBetaFast { get; set; }
    public double YarnBetaSlow { get; set; }
    public bool YarnOriginalContext { get; set; }
    public string YarnScalingType { get; set; }
    public string TypeK { get; set; }
    public string TypeV { get; set; }
    public bool NoKqvOffload { get; set; }
    public bool VocabOnly { get; set; }

    
}