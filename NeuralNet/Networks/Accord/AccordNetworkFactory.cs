using Accord.Neuro;
using Accord.Neuro.Networks;
using NeuralNet.Core.Global;
using System;

namespace NeuralNet.Networks.Accord
{
    public class AccordNetworkFactory : INetworkFactory
    {
        public INetworkInstance CreateNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(), // Activation function.
                inputCount,
                hiddenCount,
                outputCount);

            NguyenWidrow initializer = new NguyenWidrow(network);
            initializer.Randomize();

            return new AccordBPInstance(network);
        }
    }
}