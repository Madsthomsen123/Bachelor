using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Adapter.Adapter;
using Price_Simulator.Logger;

namespace Price_Simulator.TestSubjectCommunicators;

public class Consumer(IAdapter adapter, ILogger logger, ConcurrentQueue<Object> jobQueue, Stopwatch watch) : IConsumer
{
    public async Task StartConsuming(CancellationToken stoppingToken)
    {
        var priceUpdates = adapter.ConsumePriceUpdates(new Token() { StoppingToken = stoppingToken });

        //Monitor.wait()
        //while (jobQueue.IsEmpty)
        //{
        //    Thread.Sleep(1);
        //}


        await foreach (var update in priceUpdates)
        {
            logger.LogConsume(update.PriceId, update.PriceData);

            Console.WriteLine($"consumed: {update.PriceId}");
            Console.WriteLine($"time: {watch.ElapsedMilliseconds}");
            Console.WriteLine($"running on process: {Thread.GetCurrentProcessorId()}");

            //monitor.pulse
            jobQueue.Clear();
            //mointor wait
            //while (jobQueue.IsEmpty)
            //{
            //    Thread.Sleep(1);
            //}

            if(stoppingToken.IsCancellationRequested)
            {
                break;
            }

            await Task.Delay(10);


        }
    }
}