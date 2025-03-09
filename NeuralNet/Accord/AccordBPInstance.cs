using System;
using Accord.Neuro;
using Accord.Neuro.Learning;
using NeuralNet.Network;

namespace NeuralNet
{
    /// <summary>
    /// A concrete network instance using the Accord.NET library.
    /// </summary>
    public class AccordBPInstance : AbstractNetworkInstance
    {
        public ActivationNetwork Network { get; private set; }

        public AccordBPInstance(ActivationNetwork network)
            : base()
        {
            Network = network ?? throw new ArgumentNullException(nameof(network));
        }

        public override TrainingResult Train(NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback)
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

        public override double[][] Test(NetworkData data)
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
