using System.Collections.Concurrent;
using System.Diagnostics;
using Adapter.Adapter;
using Adapter.DataModels;
using Price_Simulator.Logger;

namespace Price_Simulator.TestSubjectCommunicators;

public class Publisher : IPublisher
{
    private IAdapter _adapter;
    private readonly ILogger _logger;
    private readonly ConcurrentQueue<object> _jobQueue;
    private readonly Stopwatch _watch;

    public Publisher(IAdapter adapter, ILogger logger, ConcurrentQueue<Object> jobQueue, Stopwatch watch)
    {
        _adapter = adapter;
        _logger = logger;
        _jobQueue = jobQueue;
        _watch = watch;
    }

    public async Task PublishAsync(PriceEvent price)
    {
        _logger.LogPublish(price.PriceId,
            new EnrichedPriceData() { CpuUsage = null, Price = price.PriceData.Price, RamUsage = null });
        await _adapter.PublishPrice(price);
        // _jobQueue.Enqueue(new object());

        Console.WriteLine($"published: {price.PriceId}");
        Console.WriteLine($"time: {_watch.ElapsedMilliseconds}");
        Console.WriteLine($"running on process: {Thread.GetCurrentProcessorId()}");
        await Task.Delay(10);
        //while (!_jobQueue.IsEmpty)
        //{
        //}
    }
}