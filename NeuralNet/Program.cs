using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Neuro;
using Accord.Neuro.Networks;

namespace NeuralNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the XOR dataset.
            double[][] inputs = new double[][]
            {
                new double[] { 0, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 }
            };

            double[][] outputs = new double[][]
            {
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 1 },
                new double[] { 0 }
            };

            // Create the dataset.
            NetworkData xorData = new NetworkData(inputs, outputs);

            // Create a network instance using the factory.
            NetworkInstance instance = NetworkFactory.CreateNetwork(2, 3, 1);

            // Create the runner with the network instance and dataset.
            NetworkRunner runner = new NetworkRunner(instance, xorData)
            {
                // Enable verbose logging for expert-level details.
                Verbose = true
            };

            // Run training and testing.
            runner.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
