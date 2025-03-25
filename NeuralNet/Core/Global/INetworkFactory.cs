namespace NeuralNet.Core.Global
{
    public interface INetworkFactory
    {
        public string NetworkKey => this.GetType().Name.Contains("NetworkFactory") ? GetType().Name.Split("NetworkFactory")[0] : GetType().Name;

        INetworkInstance CreateNetwork(int inputCount, int hiddenCount, int outputCount);
    }
}
