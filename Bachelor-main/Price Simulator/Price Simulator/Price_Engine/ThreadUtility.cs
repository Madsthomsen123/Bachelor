using System.Collections.Concurrent;
using System.Diagnostics;
using Adapter.Adapter;
using Adapter.DataModels;

namespace Price_Simulator.Price_Engine;

public static class ThreadUtility
{
    public static void StartThreads(List<Thread[]> threads)
    {
        foreach (var thread in threads)
        {
            thread[0].Priority = ThreadPriority.Highest;
            thread[1].Priority = ThreadPriority.Highest;
            thread[0].Start();
            thread[1].Start();
        }
    }

    public static void InitializeThreads(CancellationToken stoppingToken, Dictionary<string, IAdapter> adapters,
        Dictionary<string, (ConcurrentQueue<PriceEvent>, ConcurrentQueue<object>)> threadCommunicationQueues, Dictionary<string, Logger.Logger> loggers, List<Thread[]> threads)
    {
        foreach (var adapter in adapters)
        {
            var stopwatch = Stopwatch.StartNew();
            var logger = new Logger.Logger(adapter.Key + "log", adapter.Key);
            var consumer = new TestSubjectCommunicators.Consumer(adapter.Value, logger, threadCommunicationQueues[adapter.Key].Item2, stopwatch);
            var publisher = new TestSubjectCommunicators.Publisher(adapter.Value, logger, threadCommunicationQueues[adapter.Key].Item2, stopwatch);
            loggers[adapter.Key] = logger;

            threads.Add(new Thread[]
            {
                    new Thread( () =>
                    {
                        try
                        {
                            Console.WriteLine("calling publish");
                            while (!stoppingToken.IsCancellationRequested)
                            {
                                if (threadCommunicationQueues[adapter.Key].Item1.TryDequeue(out var priceEvent))
                                {
                                    publisher.PublishAsync(priceEvent).Wait(stoppingToken);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }),
                    new Thread(() =>
                    {
                        try
                        {
                            Console.WriteLine("calling consume");
                            consumer.StartConsuming(stoppingToken).Wait(stoppingToken);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    })
            });
        }
    }
}