using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Network
{
    public abstract class AbstractNetworkInstance : INetworkInstance
    {
        public Guid Guid { get; set; }

        public AbstractNetworkInstance()
        {
            Guid = Guid.NewGuid();
        }

        public abstract TrainingResult Train(NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback);

        public abstract double[][] Test(NetworkData data);
    }
}
