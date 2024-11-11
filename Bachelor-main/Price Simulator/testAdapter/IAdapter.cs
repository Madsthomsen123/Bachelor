using Price_Simulator.DataModels;

namespace Price_Simulator.Adapter;

public interface IAdapter
{
    Task PublishPrice(PriceData price);

    IAsyncEnumerable<EnrichedPriceData> ConsumePriceUpdates(Token stoppingToken);
}

public class Token
{
    private CancellationToken StoppingToken { get; set; }
}