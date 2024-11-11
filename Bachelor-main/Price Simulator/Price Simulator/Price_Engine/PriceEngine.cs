using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Adapter.Adapter;
using Adapter.DataModels;
using Price_Simulator.FileLoader;
using Price_Simulator.TestSubjectCommunicators;

namespace Price_Simulator.Price_Engine
{
    public class PriceEngine
    {
        CancellationTokenSource _cts = new CancellationTokenSource();

        private readonly IFileLoader _fileLoader;

        private readonly IPriceEventFactory _priceEventFactory;

        private int _counter = new int();

        public PriceEngine(IFileLoader fileLoader, IPriceEventFactory priceEventFactory)
        {
            _fileLoader = fileLoader;
            _priceEventFactory = priceEventFactory;
        }

        public async Task<Logger.Logger[]> StartHighFrequencySimulation(string folderLocation,
            CancellationToken stoppingToken)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            int counter = 0;

            var adapters = _fileLoader.GetAdapters(folderLocation);

            var threadCommunicationQueues = adapters.ToDictionary(k => k.Key,
                v => (new ConcurrentQueue<PriceEvent>(), new ConcurrentQueue<Object>()));

            var threads = new List<Thread[]>();
            var loggers = new Dictionary<string, Logger.Logger>();


            ThreadUtility.InitializeThreads(stoppingToken, adapters, threadCommunicationQueues, loggers, threads);

            ThreadUtility.StartThreads(threads);

            while (true)
            {
                await Task.Delay(1);
                var priceEvent = _priceEventFactory.Create();
                foreach (var adapter in adapters)
                {
                    threadCommunicationQueues[adapter.Key].Item1.Enqueue(priceEvent);
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }


            return loggers.Select(kvp => kvp.Value).ToArray();
        }


        public async Task<Logger.Logger[]> StartLowFrequencySimulation(string folderLocation,
            CancellationToken stoppingToken)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            var priceGenerator = new PriceEventFactory();
            var loader = new FileLoader.FileLoader();
            int counter = 0;

            var adapters = _fileLoader.GetAdapters(folderLocation);

            var threadCommunicationQueues = adapters.ToDictionary(k => k.Key,
                v => (new ConcurrentQueue<PriceEvent>(), new ConcurrentQueue<Object>()));

            var threads = new List<Thread[]>();
            var loggers = new Dictionary<string, Logger.Logger>();


            ThreadUtility.InitializeThreads(stoppingToken, adapters, threadCommunicationQueues, loggers, threads);

            ThreadUtility.StartThreads(threads);

            while (true)
            {
                await Task.Delay(100);
                var priceEvent = _priceEventFactory.Create();
                foreach (var adapter in adapters)
                {
                    threadCommunicationQueues[adapter.Key].Item1.Enqueue(priceEvent);
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }


            return loggers.Select(kvp => kvp.Value).ToArray();
        }

        public async Task<Logger.Logger[]> StartBurstFrequencySimulation(string folderLocation, CancellationToken stoppingToken)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            var priceGenerator = new PriceEventFactory();
            var loader = new FileLoader.FileLoader();
            int counter = 0;

            var adapters = _fileLoader.GetAdapters(folderLocation);

            var threadCommunicationQueues = adapters.ToDictionary(k => k.Key, v => (new ConcurrentQueue<PriceEvent>(), new ConcurrentQueue<Object>()));

            var threads = new List<Thread[]>();
            var loggers = new Dictionary<string, Logger.Logger>();


            ThreadUtility.InitializeThreads(stoppingToken, adapters, threadCommunicationQueues, loggers, threads);

            ThreadUtility.StartThreads(threads);

            while (true)
            {
                await Task.Delay(GenerateNumber());
                var priceEvent = _priceEventFactory.Create();
                foreach (var adapter in adapters)
                {
                    threadCommunicationQueues[adapter.Key].Item1.Enqueue(priceEvent);
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }


            return loggers.Select(kvp => kvp.Value).ToArray();
        }
        private int GenerateNumber()
        {
            if (_counter < 5)
            {
                _counter++;
                return 5;
            }

            if (5 <_counter && _counter < 10)
            {
                return 100;
            }
            _counter = 0;

            return 100;

        }

        

    }
}