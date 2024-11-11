using System.Reflection;
using Adapter.Adapter;

namespace Price_Simulator.FileLoader;

public class FileLoader : IFileLoader
{
    public Dictionary<string, IAdapter> GetAdapters(string filePath)
    {
        var adapterMethods = typeof(IAdapter).GetMethods();
        var files = Directory.EnumerateFiles(filePath);
        var adapterLibrarys = new List<string>();
        foreach (var file in files)
        {
            if (file.EndsWith(".dll"))
            {
                adapterLibrarys.Add(file);
            }
        }


        var assemblies = adapterLibrarys.Select(Assembly.LoadFrom);

        var types = assemblies.SelectMany(a =>
        {
            try
            {
                return a.GetExportedTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return new Type[0];
            }
        });

        var adapters = new List<Type>();

        foreach (var type in types)
        {
            var isAssignableFrom = typeof(IAdapter).IsAssignableFrom(type);
            if (isAssignableFrom && !type.IsInterface)
            {
                adapters.Add(type);
            }
        }

        return adapters.ToDictionary(a => a.Name, a => (IAdapter)a.Assembly.CreateInstance(a.FullName));
    }
}