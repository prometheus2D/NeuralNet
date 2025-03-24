using System;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Engine.Network.Activation;
using NeuralNet.Core.Global;

namespace NeuralNet.Networks.Encog
{
    public class EncogNetworkFactory : INetworkFactory
    {
        public INetworkInstance CreateNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            // Create and structure an Encog BasicNetwork.
            BasicNetwork network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, inputCount)); // Input layer (with bias)
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, hiddenCount)); // Hidden layer (with bias)
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, outputCount)); // Output layer (no bias)
            network.Structure.FinalizeStructure();
            network.Reset();

            return new EncogBPInstance(network);
        }
    }
}
