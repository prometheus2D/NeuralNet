using System;
using NeuralNet.Core.Training;
using NeuralNet.Data;

namespace NeuralNet.Core.Global
{
    public interface INetworkInstance
    {
        public Guid Guid { get; set; }

        TrainingResult Train(NetworkInstanceRunner runner, NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback);

        double[][] Test(NetworkData data);
    }
}
