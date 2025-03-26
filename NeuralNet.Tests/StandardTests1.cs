using NeuralNet.Core.Global;
using NeuralNet.Core.Training;
using NeuralNet.Data;
using NeuralNet.Networks.RonNet.RonBP;

namespace NeuralNet.Tests
{
    [TestClass]
    public class StandardTests1
    {
        [TestMethod]
        public void Test_RonBP_XOR_NonRandomInit()
        {
            var networkData = NetworkData.InitXORData();
            var networkInstance = new RonBPInstance(new int[] { networkData.InputSetLength, 3, networkData.OutputSetLength });
            var trainingParemeters = TrainingParameters.Default;

            var runner = new NetworkInstanceRunner(networkInstance, networkData, trainingParemeters, null, (index, value) =>
            {

            });

            Assert.IsTrue(true);
        }
    }
}