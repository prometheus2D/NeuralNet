using System;
using Accord.Neuro;
using Accord.Neuro.Learning;

namespace NeuralNet
{
    /// <summary>
    /// A concrete network instance using the Accord.NET library.
    /// </summary>
    public class AccordNetworkInstance : INetworkInstance
    {
        public ActivationNetwork Network { get; private set; }

        public AccordNetworkInstance(ActivationNetwork network)
        {
            Network = network ?? throw new ArgumentNullException(nameof(network));
        }

        public TrainingResult Run(NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            BackPropagationLearning teacher = new BackPropagationLearning(Network)
            {
                LearningRate = parameters.LearningRate,
                Momentum = parameters.Momentum
            };

            int iteration = 0;
            double error = double.MaxValue;
            DateTime startTime = DateTime.Now;

            while (error > parameters.ErrorThreshold && iteration < parameters.MaxIterations)
            {
                error = teacher.RunEpoch(data.Inputs, data.Outputs);
                iteration++;

                // Simply report progress every iteration.
                progressCallback?.Invoke(new TrainingProgress { Iteration = iteration, Error = error });
            }

            DateTime endTime = DateTime.Now;
            return new TrainingResult
            {
                IterationCount = iteration,
                FinalError = error,
                TrainingTime = endTime - startTime
            };
        }

        public double[][] Test(NetworkData data)
        {
            double[][] results = new double[data.Inputs.Length][];
            for (int i = 0; i < data.Inputs.Length; i++)
            {
                results[i] = Network.Compute(data.Inputs[i]);
            }
            return results;
        }
    }
}
