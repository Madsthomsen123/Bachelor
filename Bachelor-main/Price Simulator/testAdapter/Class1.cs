using Price_Simulator.Adapter;
using Price_Simulator.DataModels;
using System;
using System.Runtime.CompilerServices;

namespace testAdapter
{
    public class Class1 : IAdapter
    {
        private static readonly Random random = new Random();

        public Task PublishPrice(PriceData price)
        {
            return Task.Delay(TimeSpan.FromMilliseconds(100));
        }

        public async IAsyncEnumerable<EnrichedPriceData> ConsumePriceUpdates([EnumeratorCancellation] CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Wait for a short random period to simulate asynchronous data fetching
                await Task.Delay(random.Next(500, 1000), stoppingToken);

                yield return new EnrichedPriceData()
                    {
                        CpuUsage = "50",
                        Price = new PriceData()
                        {
                            AccumulatedVolume = 100, HighPrice = 122, Id = "id", LowPrice = 10, NumberOfTrades = 1000
                        },
                        RamUsage = "30"
                    }
                    ;
            }
        }
    }
}