using System;

namespace NeuralNet
{
    /// <summary>
    /// Wraps the network instance and handles all UI (console output) by delegating progress and test result display.
    /// </summary>
    public class NetworkRunner
    {
        public INetworkInstance Instance { get; }
        public NetworkData Data { get; }
        public TrainingParameters Parameters { get; set; } = new TrainingParameters();

        public NetworkRunner(INetworkInstance instance, NetworkData data)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public void Run()
        {
            // Provide a callback that handles when to log, based solely on runner settings.
            TrainingResult result = Instance.Run(Data, Parameters, progress =>
            {
                // Decide to log based on runner-controlled verbosity.
                if ((Parameters.Verbose && (Parameters.VerboseModulus <= 0 || progress.Iteration % Parameters.VerboseModulus == 0))
                    || progress.Iteration % 1000 == 0)
                {
                    Console.WriteLine($"Iteration: {progress.Iteration}, Current error: {progress.Error:F4}");
                }
            });

            // Final training summary is logged by the runner.
            Console.WriteLine($"\nTraining complete after {result.IterationCount} iterations. Final error: {result.FinalError:F4}");
            Console.WriteLine($"Total training time: {result.TrainingTime.TotalSeconds:F2} seconds\n");

            double[][] testResults = Instance.Test(Data);
            Console.WriteLine("Testing network:");
            for (int i = 0; i < Data.Inputs.Length; i++)
            {
                int outputValue = (int)Math.Round(testResults[i][0]);
                Console.WriteLine($"Input: [{string.Join(", ", Data.Inputs[i])}] => Output: {outputValue}");
            }
        }
    }
}
