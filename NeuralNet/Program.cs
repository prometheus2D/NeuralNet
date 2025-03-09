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
            var factory = new AccordNetworkFactory();
            INetworkInstance accordInstance = factory.CreateNetwork(2, 3, 1);
            NetworkInstanceRunner accordRunner = new NetworkInstanceRunner(accordInstance, data, null,
                line => Console.WriteLine(line)//,
                //() => { }
                );
            accordRunner.Parameters.Verbose = true;
            accordRunner.Parameters.VerboseModulus = 100;
            Console.WriteLine("Running Accord.NET network:");
            accordRunner.Run();

            //// ----- Create an Encog-based network runner -----
            //INetworkInstance encogInstance = EncogBPInstance.CreateNetwork(2, 3, 1);
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
