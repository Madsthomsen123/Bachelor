

using Adapter.DataModels;

namespace Price_Simulator.Price_Engine;

public interface IPriceEventFactory
{
    PriceEvent Create();
    PriceData CreatePriceData();
}