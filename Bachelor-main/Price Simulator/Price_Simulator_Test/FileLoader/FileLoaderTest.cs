using Adapter.Adapter;

namespace Price_Simulator_Test.FileLoader;

public class FileLoaderTest
{
    [Fact]
    public void TestGetAdapters()
    {
        // Arrange
        string filePath = "./FileLoader";
        Price_Simulator.FileLoader.FileLoader fileLoader = new Price_Simulator.FileLoader.FileLoader();
        // Act
        Dictionary<string, IAdapter> result = fileLoader.GetAdapters(filePath);
        // Assert
        Assert.True(result != null);
    }
}