using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Neuro;
using Accord.Neuro.Learning;

namespace NeuralNet
{
    /// <summary>
    /// Handles training and testing of a network using a given dataset.
    /// </summary>
    public class NetworkRunner
    {
        public NetworkInstance Instance { get; }
        public NetworkData Data { get; }

        // Global verbose flag for detailed logging.
        public bool Verbose { get; set; } = false;
        public int VerboseModulus { get; set; } = 0;

        // Training parameters.
        public double LearningRate { get; set; } = 0.1;
        public double Momentum { get; set; } = 0.0;
        public double ErrorThreshold { get; set; } = 0.01;
        public int MaxIterations { get; set; } = 10000;

        public NetworkRunner(NetworkInstance instance, NetworkData data)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Trains the network using backpropagation and then tests it.
        /// </summary>
        public void Run()
        {
            BackPropagationLearning teacher = new BackPropagationLearning(Instance.Network)
            {
                LearningRate = this.LearningRate,
                Momentum = this.Momentum
            };

            int iteration = 0;
            double error = double.MaxValue;
            double bestError = double.MaxValue;
            DateTime startTime = DateTime.Now;

            while (error > ErrorThreshold && iteration < MaxIterations)
            {
                error = teacher.RunEpoch(Data.Inputs, Data.Outputs);
                iteration++;

                // Log details based on the verbose flag.
                if ((Verbose && (VerboseModulus <= 0 || iteration % VerboseModulus == 0)) || iteration % 1000 == 0)
                {
                    Console.WriteLine($"Iteration: {iteration}, Current error: {error:F4}");
                }
            }

            DateTime endTime = DateTime.Now;
            Console.WriteLine($"\nTraining complete after {iteration} iterations. Final error: {error:F4}");
            Console.WriteLine($"Total training time: {(endTime - startTime).TotalSeconds:F2} seconds\n");

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
                double[] computed = Instance.Network.Compute(Data.Inputs[i]);
                int outputValue = (int)Math.Round(computed[0]);
                Console.WriteLine($"Input: [{string.Join(", ", Data.Inputs[i])}] => Output: {outputValue}");
            }
        }
    }
}
