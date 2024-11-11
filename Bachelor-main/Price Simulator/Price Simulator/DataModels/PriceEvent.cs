using System.Runtime.InteropServices.JavaScript;

namespace Price_Simulator.DataModels;

public record PriceEvent
{
    public required EnrichedPriceData PriceData { get; init; }
    public required Guid PriceId { get; init; }
}