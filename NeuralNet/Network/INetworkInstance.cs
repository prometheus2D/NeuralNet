using System;

namespace NeuralNet
{
    public interface INetworkInstance
    {
        public Guid Guid { get; set; }

        TrainingResult Train(NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback);

        double[][] Test(NetworkData data);
    }
}
