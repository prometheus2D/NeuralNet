using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet
{
    public class NeuralNetworkData
    {
        public double[][] Inputs { get; }
        public double[][] Outputs { get; }

        public NeuralNetworkData(double[][] inputs, double[][] outputs)
        {
            if (inputs == null || outputs == null)
                throw new ArgumentNullException("Inputs and Outputs cannot be null.");
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("The number of input samples must match the number of output samples.");

            Inputs = inputs;
            Outputs = outputs;
        }
    }
}
