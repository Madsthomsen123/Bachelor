namespace Price_Simulator.DataModels;

public record EnrichedPriceData
{
    public required PriceData Price { get; init; }
    public string? CpuUsage { get; init; }
    public string? RamUsage { get; init; }

};