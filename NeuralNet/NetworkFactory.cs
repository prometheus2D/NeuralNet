using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Neuro;
using Accord.Neuro.Networks;

namespace NeuralNet
{
    public static class NetworkFactory
    {
        public static NetworkInstance CreateNetwork(int inputCount, int hiddenCount, int outputCount)
        {
            // Create a feedforward network with the specified parameters.
            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(), // Activation function.
                inputCount,            // Number of inputs.
                hiddenCount,           // Number of neurons in the hidden layer.
                outputCount);          // Number of outputs.

            // Initialize the network's weights using Nguyen-Widrow.
            NguyenWidrow initializer = new NguyenWidrow(network);
            initializer.Randomize();

            return new NetworkInstance(network);
        }
    }
}
