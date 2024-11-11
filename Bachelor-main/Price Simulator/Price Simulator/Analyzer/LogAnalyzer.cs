using Price_Simulator.Logger;

namespace Price_Simulator.Analyzer;

public static class LogAnalyzer
{
    public static IReadOnlyCollection<LogResult> AnalyzeLogs(ILogger[] logs)
    {
        var results = new List<LogResult>();
        foreach (var log in logs)
        {
            var frozenDictionaries = log.GetLogs();


            // Calculate average time difference between logs
            var timeDifferences = frozenDictionaries[1]
                .Where(entry => frozenDictionaries[0].ContainsKey(entry.Key))
                .Select(entry => entry.Value.ElapsedMilliseconds - frozenDictionaries[0][entry.Key].ElapsedMilliseconds);

            var averageTimeDifference = timeDifferences.Average();


            var averageCpuUsed = frozenDictionaries[1]
                .Select(entry => frozenDictionaries[1][entry.Key].Data.CpuUsage)
                .Average()!.Value;

            var averageRamUsed = frozenDictionaries[1]
                .Select(entry => entry.Value.Data.RamUsage)
                .Where(value => value.HasValue)
                .Average()!.Value;

            var errors = Math.Abs(frozenDictionaries[1].Keys.Length - frozenDictionaries[0].Keys.Length);

            results.Add(new LogResult { AverageCpuUsage = averageCpuUsed, AverageRamUsage = averageRamUsed, AverageTimeDifference = averageTimeDifference, Errors = errors, SystemName = log.SystemName});
        }

        return results;
    }
}