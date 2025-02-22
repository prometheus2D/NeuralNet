using System;
using System.Diagnostics;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Engine.Network.Activation;
using Encog.Neural.Networks.Training.Propagation.Back;

namespace NeuralNet
{
    public class EncogNetworkInstance : INetworkInstance
    {
        public BasicNetwork Network { get; private set; }

        public EncogNetworkInstance(BasicNetwork network)
        {
            Network = network ?? throw new ArgumentNullException(nameof(network));
        }

        public TrainingResult Run(NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            // Convert the input/output arrays into an Encog MLDataSet.
            IMLDataSet trainingSet = new BasicMLDataSet(data.Inputs, data.Outputs);

            // Create the backpropagation trainer.
            var trainer = new Backpropagation(Network, trainingSet, parameters.LearningRate, parameters.Momentum);

            int iteration = 0;
            double error = double.MaxValue;
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (error > parameters.ErrorThreshold && iteration < parameters.MaxIterations)
            {
                trainer.Iteration(); // Performs one training iteration.
                error = trainer.Error;
                iteration++;

                // Report progress every iteration.
                progressCallback?.Invoke(new TrainingProgress { Iteration = iteration, Error = error });
            }

            stopwatch.Stop();
            return new TrainingResult
            {
                IterationCount = iteration,
                FinalError = error,
                TrainingTime = stopwatch.Elapsed
            };
        }

        public double[][] Test(NetworkData data)
        {
            double[][] results = new double[data.Inputs.Length][];
            for (int i = 0; i < data.Inputs.Length; i++)
            {
                // Compute output for each input vector.
                IMLData inputData = new BasicMLData(data.Inputs[i]);
                IMLData outputData = Network.Compute(inputData);
                results[i] = outputData == null ? throw new Exception() : (outputData as BasicMLData).Data;
            }
            return results;
        }
    }
}
