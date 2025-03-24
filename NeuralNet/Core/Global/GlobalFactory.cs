using NeuralNet.Networks.Accord;
using NeuralNet.Networks.Encog;
using NeuralNet.Networks.RonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Core.Global
{
    public class GlobalFactory
    {
        public static Dictionary<string, INetworkFactory> NetworkFactories { get; } = new Dictionary<string, INetworkFactory>()
        {
            { "accord", new AccordNetworkFactory() },
            { "encog", new EncogNetworkFactory() },
            { "ron", new RonNetworkFactory() }
        };

        public static INetworkInstance CreateNetworkInstance(string networkType, string optType, int[] network)
        {
            var networkFactory = NetworkFactories[networkType.ToLower()] ?? throw new Exception();

            var result = networkFactory.CreateNetwork(network[0], network[1], network[2]);

            return result;
        }
    }
}
