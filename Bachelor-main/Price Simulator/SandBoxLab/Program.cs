using System.Diagnostics;
using Price_Simulator.FileLoader;
using Price_Simulator.Price_Engine;

namespace SandBoxLab;

internal class Program
{
    public static void Main(string[] args)
    {
        var priceEngine = new PriceEngine(new FileLoader(), new PriceEventFactory());

        var cts = new CancellationTokenSource();

        cts.CancelAfter(5000);

        var loggers = priceEngine.StartHighFrequencySimulation("C:\\Users\\BG5521\\OneDrive - Danske Bank A S\\Desktop",
            cts.Token);

        var loggersResult = loggers.Result;

        foreach (var logger in loggersResult)
        {
            var frozenDictionaries = logger.GetLogs();

            var enumerable = frozenDictionaries[1].Keys.Select(k =>
                frozenDictionaries[1][k].ElapsedMilliseconds - frozenDictionaries[0][k].ElapsedMilliseconds);

            Console.WriteLine($"avereage was {enumerable.Average()}");
        }
    }
}