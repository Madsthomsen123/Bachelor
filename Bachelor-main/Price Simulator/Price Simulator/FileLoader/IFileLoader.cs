using Adapter.Adapter;

namespace Price_Simulator.FileLoader;

public interface IFileLoader
{
    Dictionary<string, IAdapter> GetAdapters(string filePath);
}