using System;
using Accord.Neuro;
using Accord.Neuro.Networks;

namespace NeuralNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a network instance using the factory abstraction.
            INetworkFactory factory = new AccordNetworkFactory();
            INetworkInstance instance = factory.CreateNetwork(2, 2, 1);

            // Create the runner with the instance and dataset.
            NetworkRunner runner = new NetworkRunner(instance, NetworkData.XORData);

            // Configure training parameters.
            runner.Parameters.Verbose = true;
            runner.Parameters.VerboseModulus = 10;

            // Run the training and testing, with all UI handled by the runner.
            runner.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
