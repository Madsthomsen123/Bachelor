using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcPriceService.Protos;
using GrpcPriceService.Queue;

namespace GrpcPriceService.Services;

public class PriceService(IMessageQueue queue) : PriceStream.PriceStreamBase
{
    private readonly IMessageQueue _queue = queue;

    public override async Task PricesStream(Empty request, IServerStreamWriter<gRPCPriceEvent> responseStream, ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(20);
            var gRpcPriceEvent = _queue.GetNext();
            await responseStream.WriteAsync(gRpcPriceEvent);
            Console.WriteLine($"published: {gRpcPriceEvent.EventId}");
        }
    }

    public override async Task<Empty> PublishPrice(gRPCPriceEvent request, ServerCallContext context)
    {
        await Task.Delay(20);
        _queue.Add(request);
        Console.WriteLine($"added price: { request.EventId} to stream");
        return new Empty();
    }
}