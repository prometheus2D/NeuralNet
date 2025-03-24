namespace NeuralNet.Core.Global
{
    public interface INetworkFactory
    {
        INetworkInstance CreateNetwork(int inputCount, int hiddenCount, int outputCount);
    }
}
