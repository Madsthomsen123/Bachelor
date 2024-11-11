using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using Adapter.DataModels;
using Microsoft.Extensions.Logging;

namespace Price_Simulator.Logger;

public class Logger : ILogger
{
    public string SystemName { get; }
    private readonly string? _filePath;
    private readonly string _fileName;
    private readonly ConcurrentDictionary<string, LogEntry> _publishLogs = new();
    private readonly ConcurrentDictionary<string, LogEntry> _consumeLogs = new();
    private List<LogEntry[]> _logEntries = new();
    public int PublishCounter { get; private set; }
    public int ConsumeCounter { get; private set; }

    private readonly Stopwatch _watch = new();

    public Logger(string fileName, string systemName)
    {
        SystemName = systemName;
        _fileName = fileName;
        _filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        _watch.Start();
    }

    public void LogPublish(string id, EnrichedPriceData data)
    {
        
            _publishLogs[id] = new LogEntry(_watch.ElapsedMilliseconds,
                data);
            PublishCounter++;
        
    }

    public void LogConsume(string id, EnrichedPriceData data)
    {
        
            _consumeLogs[id] = new LogEntry(_watch.ElapsedMilliseconds, data);
            ConsumeCounter++;
    }

    public void Dump(string folderLocation)
    {
        var combinedLogs = _publishLogs.Join(_consumeLogs,
            pair => pair.Key,
            pair => pair.Key,
            (publishPair, consumePair) => new Dictionary<string, LogEntry>
            {
                { "published", publishPair.Value },
                { "consumed", consumePair.Value }
            });
        var json = JsonSerializer.Serialize(combinedLogs);
        var location = folderLocation + $"\\{_fileName}.json";
        File.WriteAllText(location, json);
    }

    public FrozenDictionary<string, LogEntry>[] GetLogs()
    {
        return [_publishLogs.ToFrozenDictionary(), _consumeLogs.ToFrozenDictionary()];
    } 
}