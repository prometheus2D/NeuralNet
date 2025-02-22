using System;
using Accord.Neuro;
using Accord.Neuro.Networks;
using Encog.App.Quant.Loader.OpenQuant.Data;
using Encog.Bot.Browse.Range;

namespace NeuralNet
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkData data = NetworkData.XORData;

            // ----- Create an Accord-based network runner -----
            INetworkFactory accordFactory = new AccordNetworkFactory(); // Assume this exists from previous code.
            INetworkInstance accordInstance = accordFactory.CreateNetwork(2, 2, 1);
            NetworkRunner accordRunner = new NetworkRunner(accordInstance, data);
            accordRunner.Parameters.Verbose = true;
            accordRunner.Parameters.VerboseModulus = 100;
            Console.WriteLine("Running Accord.NET network:");
            accordRunner.Run();

            // ----- Create an Encog-based network runner -----
            INetworkFactory encogFactory = new EncogNetworkFactory();
            INetworkInstance encogInstance = encogFactory.CreateNetwork(2, 2, 1);
            NetworkRunner encogRunner = new NetworkRunner(encogInstance, data);
            encogRunner.Parameters.Verbose = true;
            encogRunner.Parameters.VerboseModulus = 100;
            Console.WriteLine("\nRunning Encog network:");
            encogRunner.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
