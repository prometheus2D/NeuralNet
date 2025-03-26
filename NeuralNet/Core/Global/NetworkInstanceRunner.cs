using System;
using System.Collections.Concurrent;
using System.Text;
using Accord.Statistics.Running;
using NeuralNet.Core.Training;
using NeuralNet.Data;

namespace NeuralNet.Core.Global
{
    public class NetworkInstanceRunner
    {
        public INetworkInstance Instance { get; }
        public NetworkData Data { get; }
        public TrainingParameters Parameters { get; set; }
        public bool IsFinished { get; private set; } = false;
        public bool IsAborted { get; private set; } = false;

        public delegate void TrainIterationEventHandler(int index, double value);
        public event TrainIterationEventHandler TrainIterationEvent;
        public delegate void LogEventHandler(string line);
        public event LogEventHandler LogEvent;

        private ConcurrentQueue<string> _LogQueue { get; } = new();
        private ConcurrentQueue<(int Iteration, double Error)> _IterationQueue { get; } = new();

        public NetworkInstanceRunner(INetworkInstance instance, NetworkData data, TrainingParameters parameters,
                    LogEventHandler logEvent = null, TrainIterationEventHandler trainEvent = null)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Parameters = parameters ?? TrainingParameters.Default;

            LogEvent += logEvent;
            TrainIterationEvent += trainEvent;
        }

        public List<double> testOutput = new();
        public void ProcessEvents()
        {
            if (LogEvent != null)
                while (!_LogQueue.IsEmpty)
                    if (_LogQueue.TryDequeue(out string logLine))
                        LogEvent.Invoke(logLine);

            if (TrainIterationEvent != null)
                while (!_IterationQueue.IsEmpty)
                    if (_IterationQueue.TryDequeue(out (int Iteration, double Error) iterationData))
                    {
                        TrainIterationEvent.Invoke(iterationData.Iteration, iterationData.Error);
                    }
        }
        public void QueueLogEvent(string line)
        {
            if (LogEvent != null)
                _LogQueue.Enqueue(line);
        }
        public void QueueTrainIterationEvent(int index, double value)
        {
            if (TrainIterationEvent != null)
                _IterationQueue.Enqueue((index, value));
        }

        public Task RunTask { get; set; }
        public void Abort() => IsAborted = true;
        public Task Run() => Task.Run(_Run);
        private void _Run()
        {
            IsFinished = false;

            TrainingResult result = Instance.Train(Data, Parameters, progress =>
            {
                if (Parameters.Verbose && (Parameters.VerboseModulus <= 0 || progress.Iteration % Parameters.VerboseModulus == 0)
                    || progress.Iteration % 1000 == 0)
                {
                    QueueLogEvent($"Iteration: {progress.Iteration}, Current error: {progress.Error:F4}");
                }
                QueueTrainIterationEvent(progress.Iteration, progress.Error);
            });

            QueueLogEvent($"\nTraining complete after {result.IterationCount} iterations. Final error: {result.FinalError:F4}");
            QueueLogEvent($"Total training time: {result.TrainingTime.TotalSeconds:F2} seconds\n");

            double[][] testResults = Instance.Test(Data);
            QueueLogEvent("Testing network:");
            for (int i = 0; i < Data.Inputs.Length; i++)
            {
                int outputValue = (int)Math.Round(testResults[i][0]);
                QueueLogEvent($"Input: [{string.Join(", ", Data.Inputs[i])}] => Output: {outputValue}");
            }

            IsFinished = true;
        }
    }
}
