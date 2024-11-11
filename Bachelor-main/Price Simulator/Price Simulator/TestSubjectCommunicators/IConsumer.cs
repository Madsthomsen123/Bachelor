namespace Price_Simulator.TestSubjectCommunicators;

public interface IConsumer
{
    Task StartConsuming(CancellationToken stoppingToken);
}