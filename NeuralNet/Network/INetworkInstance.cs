using System;

namespace NeuralNet
{
    public interface INetworkInstance
    {
        TrainingResult Run(NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback);

        double[][] Test(NetworkData data);
    }
}
