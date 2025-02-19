using Accord.Neuro;
using Accord.Neuro.Networks;
using System;

namespace NeuralNet
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

            return new AccordNetworkInstance(network);
        }
    }
}