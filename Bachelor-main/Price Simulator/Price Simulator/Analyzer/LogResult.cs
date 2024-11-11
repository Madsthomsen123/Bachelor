namespace Price_Simulator.Analyzer;

public class LogResult
{
    public string SystemName { get; set; }
    public double AverageTimeDifference { get; set; }
    public double AverageCpuUsage { get; set; }
    public double AverageRamUsage { get; set; }
    public int Errors { get; set; }
}