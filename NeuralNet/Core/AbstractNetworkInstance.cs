using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNet.Core.Global;
using NeuralNet.Core.Training;
using NeuralNet.Data;

namespace NeuralNet.Core
{
    public abstract class AbstractNetworkInstance : INetworkInstance
    {
        public Guid Guid { get; set; }

        public AbstractNetworkInstance()
        {
            Guid = Guid.NewGuid();
        }

        public abstract TrainingResult Train(NetworkInstanceRunner runner, NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback);

        public abstract double[][] Test(NetworkData data);
    }
}
