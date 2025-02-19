using Accord.Neuro.Learning;
using Accord.Neuro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet
{
    public class NeuralNetworkRunner
    {
        public ActivationNetwork Network { get; }
        public NeuralNetworkData Data { get; }

        // Training parameters.
        public double LearningRate { get; set; } = 0.1;
        public double Momentum { get; set; } = 0.0;
        public double ErrorThreshold { get; set; } = 0.01;
        public int MaxIterations { get; set; } = 10000;

        public NeuralNetworkRunner(ActivationNetwork network, NeuralNetworkData data)
        {
            Network = network ?? throw new ArgumentNullException(nameof(network));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Trains the network using backpropagation and then tests it.
        /// </summary>
        public void Run()
        {
            BackPropagationLearning teacher = new BackPropagationLearning(Network)
            {
                LearningRate = this.LearningRate,
                Momentum = this.Momentum
            };

            int iteration = 0;
            double error = double.MaxValue;

            // Training loop.
            while (error > ErrorThreshold && iteration < MaxIterations)
            {
                error = teacher.RunEpoch(Data.Inputs, Data.Outputs);
                iteration++;

                if (iteration % 1000 == 0)
                {
                    Console.WriteLine($"Iteration: {iteration}, Error: {error:F4}");
                }
            }

            Console.WriteLine($"\nTraining complete after {iteration} iterations. Final error: {error:F4}\n");
            Test();
        }

        /// <summary>
        /// Tests the trained network on the provided dataset.
        /// </summary>
        public void Test()
        {
            Console.WriteLine("Testing network:");
            for (int i = 0; i < Data.Inputs.Length; i++)
            {
                double[] computed = Network.Compute(Data.Inputs[i]);
                int outputValue = (int)Math.Round(computed[0]);
                Console.WriteLine($"Input: [{string.Join(", ", Data.Inputs[i])}] => Output: {outputValue}");
            }
        }
    }
}
