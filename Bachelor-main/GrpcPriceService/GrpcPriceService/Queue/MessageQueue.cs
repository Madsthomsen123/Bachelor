using System.Collections.Concurrent;
using GrpcPriceService.Protos;

namespace GrpcPriceService.Queue;

public class MessageQueue() : IMessageQueue
{


    private ConcurrentQueue<gRPCPriceEvent> _queue = new();

    public async void Add(gRPCPriceEvent price)
    {
        while (!_queue.IsEmpty)
        {
            await Task.Delay(10);
        }
        _queue.Enqueue(price);
    }

    public gRPCPriceEvent GetNext()
    {
        while (_queue.Count < 1)
        {
            Task.Delay(10);
        }

        

        _queue.TryDequeue(out var result);

        return result;
    }
}