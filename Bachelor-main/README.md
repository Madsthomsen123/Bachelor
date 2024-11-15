Bachelor 

The simulator is designed to generate and manage mock events in real-time to assess the performance of various streaming technologies in a financial context. The main goal is to simulate real-time price updates by producing a stream of data at varying frequencies and patterns, sent to multiple systems to compare efficiency under dynamic conditions.

The system will be flexible and modular, suitable for a variety of tests. It includes an event generator that simulates diverse data patterns, such as sudden data bursts, reflecting unpredictable market conditions. This allows for evaluation of different technologies’ performance under changing load conditions and provides insights into how these technologies handle stress and respond to different workloads.

The system will enable measurement and analysis of key performance parameters, including latency, error rate, and resource consumption. By logging and collecting data in real time, the simulator will provide valuable insights into how each technology handles load and responds under varying operational conditions.

The simulator interacts with different technologies through an adapter, which is implemented separately for each technology. Project files are loaded by the simulator, which then runs tests with these implementations.

Streaming technology tests will be managed through a user interface where the user can select which tests to run and upload their adapter .dll files. The tests themselves will be executed through a multi-threaded system.
