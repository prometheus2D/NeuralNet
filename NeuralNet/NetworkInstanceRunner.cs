using System;

namespace NeuralNet
{
    public class NetworkInstanceRunner
    {
        public INetworkInstance Instance { get; }
        public NetworkData Data { get; }
        public TrainingParameters Parameters { get; set; }

        public NetworkInstanceRunner(INetworkInstance instance, NetworkData data, TrainingParameters parameters,
            LogEventHandler logEvent = null, TrainIterationEventHandler trainEvent = null)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Parameters = parameters ?? new TrainingParameters();

            LogEvent += logEvent;
            TrainIterationEvent += trainEvent;
        }

        public delegate void TrainIterationEventHandler(int index, double value);
        public event TrainIterationEventHandler TrainIterationEvent;

        public delegate void LogEventHandler(string line);
        public event LogEventHandler LogEvent;

        public void Run()
        {
            // Provide a callback that handles when to log, based solely on runner settings.
            TrainingResult result = Instance.Train(Data, Parameters, progress =>
            {
                // Decide to log based on runner-controlled verbosity.
                if ((Parameters.Verbose && (Parameters.VerboseModulus <= 0 || progress.Iteration % Parameters.VerboseModulus == 0))
                    || progress.Iteration % 1000 == 0)
                {
                    //Console.WriteLine($"Iteration: {progress.Iteration}, Current error: {progress.Error:F4}");
                    LogEvent?.Invoke($"Iteration: {progress.Iteration}, Current error: {progress.Error:F4}");
                }
                TrainIterationEvent?.Invoke(progress.Iteration, progress.Error);
            });

            // Final training summary is logged by the runner.
            LogEvent?.Invoke($"\nTraining complete after {result.IterationCount} iterations. Final error: {result.FinalError:F4}");
            LogEvent?.Invoke($"Total training time: {result.TrainingTime.TotalSeconds:F2} seconds\n");

            double[][] testResults = Instance.Test(Data);
            LogEvent?.Invoke("Testing network:");
            for (int i = 0; i < Data.Inputs.Length; i++)
            {
                int outputValue = (int)Math.Round(testResults[i][0]);
                LogEvent?.Invoke($"Input: [{string.Join(", ", Data.Inputs[i])}] => Output: {outputValue}");
            }
        }
    }
}
