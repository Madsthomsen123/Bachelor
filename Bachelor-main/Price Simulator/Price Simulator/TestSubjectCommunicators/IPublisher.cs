

using Adapter.DataModels;

namespace Price_Simulator.TestSubjectCommunicators;

public interface IPublisher
{
    Task PublishAsync(PriceEvent price);
}