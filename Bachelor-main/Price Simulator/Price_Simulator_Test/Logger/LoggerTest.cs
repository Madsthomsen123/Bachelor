using Adapter.DataModels;
using System.Reflection;

namespace Price_Simulator_Test.Logger
{
    public class LoggerTest
    {
        [Fact]
        public void LoggerShouldLogCorrect()
        {
            //Arrange
            var data1 = new PriceData()
            {
                AccumulatedVolume = 2,
                LowPrice = 2,
                HighPrice = 4,
                Id = "id2",
                NumberOfTrades = 123,
                TradeDate = 1234,
                TradePrice = 1234,
                TradeVolume = 23333
            };

            var uut = new Price_Simulator.Logger.Logger("filename", "systemname");


            //Act
            Thread t1 = new Thread(() =>
            {
                uut.LogPublish(data1.Id, new EnrichedPriceData() { Price = data1, CpuUsage = null, RamUsage = null });
                uut.LogPublish(data1.Id, new EnrichedPriceData() { Price = data1, CpuUsage = null, RamUsage = null });
            });


            Thread t2 = new Thread(() =>
            {
                uut.LogConsume(data1.Id, new EnrichedPriceData() { CpuUsage = 10, Price = data1, RamUsage = 123 });
                uut.LogConsume(data1.Id, new EnrichedPriceData() { CpuUsage = 10, Price = data1, RamUsage = 123 });
            });

            t2.Start();
            t1.Start();

            t1.Join();

            //Assert
            var logEntriesList = uut.GetLogs();

            Assert.NotNull(uut);

            Assert.NotNull(logEntriesList);

            foreach (var entry in logEntriesList)
            {
                
                Assert.Equal(entry[data1.Id].Data.Price, data1);
            }
        }


        public void DumpShouldWork()
        {
            //Arrange
            var data1 = new PriceData()
            {
                AccumulatedVolume = 2,
                LowPrice = 2,
                HighPrice = 4,
                Id = "id2",
                NumberOfTrades = 123,
                TradeDate = 1234,
                TradePrice = 1234,
                TradeVolume = 23333
            };

            var uut = new Price_Simulator.Logger.Logger("filename", "systemname");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //Act
            uut.LogPublish(data1.Id, new EnrichedPriceData() { Price = data1, CpuUsage = null, RamUsage = null });
            uut.LogPublish(data1.Id, new EnrichedPriceData() { Price = data1, CpuUsage = null, RamUsage = null });
            uut.LogPublish(data1.Id, new EnrichedPriceData() { Price = data1, CpuUsage = null, RamUsage = null });


            uut.LogConsume(data1.Id, new EnrichedPriceData() {CpuUsage = 10, Price = data1, RamUsage = 123 });

            //Assert

            Assert.Equal(uut.PublishCounter, 3);

            Assert.Equal(uut.ConsumeCounter, 1);
        }
    }
}