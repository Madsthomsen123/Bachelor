using GrpcPriceService.Protos;

namespace GrpcPriceService.Queue;

public interface IMessageQueue
{
    void Add(gRPCPriceEvent price);
    gRPCPriceEvent GetNext();
}