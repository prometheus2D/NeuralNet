using Accord.Neuro;
using Accord.Neuro.Networks;
using NeuralNet.Core.Global;
using System;

namespace NeuralNet.Networks.RonNet
{
    public class RonNetworkFactory : INetworkFactory
    {
        public INetworkInstance CreateNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            // Define layer structure: input, hidden, and output layers
            int[] layers = new int[] { inputCount, hiddenCount, outputCount };

            // Create and return a new RonBPInstance with the specified structure
            return new RonBPInstance(layers);
        }
    }
}