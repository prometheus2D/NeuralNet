namespace NeuralNet
{
    public interface INetworkFactory
    {
        INetworkInstance CreateNetwork(int inputCount, int hiddenCount, int outputCount);
    }
}
