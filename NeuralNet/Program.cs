﻿using System;
using NeuralNet.Core.Global;
using NeuralNet.Data;
using NeuralNet.Networks.Accord;
using NeuralNet.Networks.Accord.AccordBP;

namespace NeuralNet
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkData data = NetworkData.InitXORData();

            // ----- Create an Accord-based network runner -----
            var factory = new AccordBPNetworkFactory();
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
