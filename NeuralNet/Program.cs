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
            INetworkInstance accordInstance = accordFactory.CreateNetwork(2, 3, 1);
            NetworkInstanceRunner accordRunner = new NetworkInstanceRunner(accordInstance, data, 
                line => Console.WriteLine(line),
                () => { });
            accordRunner.Parameters.Verbose = true;
            accordRunner.Parameters.VerboseModulus = 100;
            Console.WriteLine("Running Accord.NET network:");
            accordRunner.Run();

            //// ----- Create an Encog-based network runner -----
            //INetworkFactory encogFactory = new EncogNetworkFactory();
            //INetworkInstance encogInstance = encogFactory.CreateNetwork(2, 3, 1);
            //NetworkInstanceRunner encogRunner = new NetworkInstanceRunner(encogInstance, data);
            //encogRunner.Parameters.Verbose = true;
            //encogRunner.Parameters.VerboseModulus = 100;
            //Console.WriteLine("\nRunning Encog network:");
            //encogRunner.Run();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
