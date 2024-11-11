using System.Collections.Frozen;
using Adapter.DataModels;
using Moq;
using Price_Simulator.Analyzer;
using Price_Simulator.Logger;

namespace Price_Simulator_Test.Analyzer;

public class LogAnalyzerTest
{
    [Fact]
    public void AnalyzeLogs_ReturnsCorrectAnalysisResults()
    {
        // Arrange
        var mockLog1 = new Mock<ILogger>();
        var frozenDict1 = new Dictionary<string, LogEntry>
        {
            {
                "Operation1", new LogEntry(100, new EnrichedPriceData()
                {
                    CpuUsage = 50, Price = new PriceData()
                    {
                        AccumulatedVolume = 1,
                        HighPrice = 1,
                        Id = "1",
                        LowPrice = 2,
                        NumberOfTrades = 3,
                        TradeDate = 4,
                        TradePrice = 50,
                        TradeVolume = 234
                    }, RamUsage = 50
                })
            },
            {
                "Operation2", new LogEntry(150, new EnrichedPriceData()
                {
                    CpuUsage = 50, Price = new PriceData()
                    {
                        AccumulatedVolume = 1,
                        HighPrice = 1,
                        Id = "1",
                        LowPrice = 2,
                        NumberOfTrades = 3,
                        TradeDate = 4,
                        TradePrice = 50,
                        TradeVolume = 234
                    },RamUsage = 50
                })
            }
        };

        var frozenDict2 = new Dictionary<string, LogEntry>
        {
            {
                "Operation1", new LogEntry(150, new EnrichedPriceData()
                {
                    CpuUsage =110, Price = new PriceData()
                    {
                        AccumulatedVolume = 1,
                        HighPrice = 1,
                        Id = "1",
                        LowPrice = 2,
                        NumberOfTrades = 3,
                        TradeDate = 4,
                        TradePrice = 50,
                        TradeVolume = 234
                    }, RamUsage = 110
                })
            },
            {
                "Operation2", new LogEntry(200, new EnrichedPriceData()
                {
                    CpuUsage = 110, Price = new PriceData()
                    {
                        AccumulatedVolume = 1,
                        HighPrice = 1,
                        Id = "1",
                        LowPrice = 2,
                        NumberOfTrades = 3,
                        TradeDate = 4,
                        TradePrice = 50,
                        TradeVolume = 234
                    }, RamUsage = 110
                })
            }
        };

        mockLog1.Setup(m => m.GetLogs()).Returns([frozenDict1.ToFrozenDictionary(), frozenDict2.ToFrozenDictionary()]);
        
        mockLog1.Setup(m => m.SystemName).Returns("TestSystem");



        // Act
        var results = LogAnalyzer.AnalyzeLogs([mockLog1.Object]);

        // Assert
        Assert.Single(results);
        var result = results.First();
        Assert.Equal("TestSystem", result.SystemName);
        Assert.Equal(50, result.AverageTimeDifference);
        Assert.Equal(110, result.AverageCpuUsage);
        Assert.Equal(110, result.AverageRamUsage);
        Assert.Equal(0, result.Errors);
    }
}