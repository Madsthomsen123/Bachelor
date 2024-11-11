using Adapter.Adapter;
using Adapter.DataModels;
using Moq;
using Price_Simulator.FileLoader;
using Price_Simulator.Price_Engine;

namespace Price_Simulator_Test.Price_Engine;

public class PriceEngineTest
{
    [Fact]
    public async Task StartHighFrequencySimulation_ProcessesEventsCorrectly()
    {
        var fileLoader = new Mock<IFileLoader>();

        var priceEventMock = new Mock<IPriceEventFactory>();

        priceEventMock.Setup(x => x.Create())
            .Returns(new PriceEvent
            {
                PriceData = new EnrichedPriceData()
                {
                    CpuUsage = null,
                    Price = new PriceData()
                    {
                        AccumulatedVolume = 1,
                        HighPrice = 1,
                        Id = "1",
                        LowPrice = 2,
                        NumberOfTrades = 3,
                        TradeDate = 4,
                        TradePrice = 50,
                        TradeVolume = 234
                    },
                    RamUsage = 123
                },
                PriceId = "12"
            });
        fileLoader.Setup(x => x.GetAdapters(It.IsAny<string>()))
            .Returns(new Dictionary<string, IAdapter>
            {
                {
                    "adapter1", new Mock<IAdapter>().Object
                }
            });

        var engine = new PriceEngine(fileLoader.Object, priceEventMock.Object);
        var cts = new CancellationTokenSource();

        var task = engine.StartHighFrequencySimulation("path", cts.Token);

        // Simulate a short run
        await Task.Delay(10);
        cts.Cancel();

        var result = await task;

        // Check if loggers are returned correctly
        Assert.NotNull(result);
    }

    [Fact]
    public async Task StartLowFrequncySimulation_processeseventscorrectly()
    {
        var fileLoader = new Mock<IFileLoader>();

        var priceEventMock = new Mock<IPriceEventFactory>();

        priceEventMock.Setup(x => x.Create())
            .Returns(new PriceEvent
            {
                PriceData = new EnrichedPriceData()
                {
                    CpuUsage = null,
                    Price = new PriceData()
                    {
                        AccumulatedVolume = 1,
                        HighPrice = 1,
                        Id = "1",
                        LowPrice = 2,
                        NumberOfTrades = 3,
                        TradeDate = 4,
                        TradePrice = 50,
                        TradeVolume = 234
                    },
                    RamUsage = 123
                },
                PriceId = "12"
            });
        fileLoader.Setup(x => x.GetAdapters(It.IsAny<string>()))
            .Returns(new Dictionary<string, IAdapter>
            {
                {
                    "adapter1", new Mock<IAdapter>().Object
                }
            });

        var engine = new PriceEngine(fileLoader.Object, priceEventMock.Object);
        var cts = new CancellationTokenSource();

        var task = engine.StartLowFrequencySimulation("path", cts.Token);

        // Simulate a short run
        await Task.Delay(10);
        cts.Cancel();

        var result = await task;

        // Check if loggers are returned correctly
        Assert.NotNull(result);
    }

    [Fact]
    public async Task StartBurstFrequencySimulation()
    {
                var fileLoader = new Mock<IFileLoader>();

        var priceEventMock = new Mock<IPriceEventFactory>();

        priceEventMock.Setup(x => x.Create())
            .Returns(new PriceEvent
            {
                PriceData = new EnrichedPriceData()
                {
                    CpuUsage = null,
                    Price = new PriceData()
                    {
                        AccumulatedVolume = 1,
                        HighPrice = 1,
                        Id = "1",
                        LowPrice = 2,
                        NumberOfTrades = 3,
                        TradeDate = 4,
                        TradePrice = 50,
                        TradeVolume = 234
                    },
                    RamUsage = 123
                },
                PriceId = "12"
            });
        fileLoader.Setup(x => x.GetAdapters(It.IsAny<string>()))
            .Returns(new Dictionary<string, IAdapter>
            {
                {
                    "adapter1", new Mock<IAdapter>().Object
                }
            });

        var engine = new PriceEngine(fileLoader.Object, priceEventMock.Object);
        var cts = new CancellationTokenSource();

        var task = engine.StartBurstFrequencySimulation("path", cts.Token);

        // Simulate a short run
        await Task.Delay(10);
        cts.Cancel();

        var result = await task;

        // Check if loggers are returned correctly
        Assert.NotNull(result);
    }
}