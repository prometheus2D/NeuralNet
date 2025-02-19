using Accord.Neuro;
using Accord.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNet
{
    public class Program
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
            NeuralNetworkData xorData = new NeuralNetworkData(inputs, outputs);

            // Create a feedforward network with 2 inputs, 2 hidden neurons, and 1 output.
            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(), // Activation function.
                2,                     // Number of inputs.
                2,                     // Number of neurons in the hidden layer.
                1);                    // Number of outputs.

            // Initialize the network's weights using Nguyen-Widrow.
            NguyenWidrow initializer = new NguyenWidrow(network);
            initializer.Randomize();

            // Create the runner by combining the network and dataset.
            NeuralNetworkRunner runner = new NeuralNetworkRunner(network, xorData);

            // Run training and testing.
            runner.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
