using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Neuro;
using Accord.Neuro.Networks;

namespace NeuralNet
{
    public class NetworkInstance
    {
        public ActivationNetwork Network { get; private set; }

        public NetworkInstance(ActivationNetwork network)
        {
            Network = network ?? throw new ArgumentNullException(nameof(network));
        }
    }
}
