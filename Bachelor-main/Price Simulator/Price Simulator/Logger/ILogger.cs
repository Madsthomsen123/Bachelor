
using System.Collections.Frozen;
using Adapter.DataModels;

namespace Price_Simulator.Logger;

public interface ILogger
{
    // Method to log the publishing of data
    void LogPublish(string id, EnrichedPriceData data);

    // Method to log the consumption of data
    void LogConsume(string id, EnrichedPriceData data);

    // Method to dump logs to a file
    void Dump(string fileLocation);

    string SystemName { get; }

    // Method to retrieve all logged entries
    FrozenDictionary<string, LogEntry>[] GetLogs();
}