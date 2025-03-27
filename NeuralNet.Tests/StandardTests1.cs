using NeuralNet.Core.Global;
using NeuralNet.Core.Training;
using NeuralNet.Data;
using NeuralNet.Networks.RonNet.RonBP;

namespace NeuralNet.Tests
{
    [TestClass]
    public class StandardTests1
    {
        public static bool WithinRange(double value, double target, double range) => (value >= target - range && value <= target + range);

        [TestMethod]
        public void Test_RonBP_XOR_NonRandomInit()
        {
            var networkData = NetworkData.InitXORData();
            var networkInstance = new RonBPInstance(new int[] { networkData.InputSetLength, 3, networkData.OutputSetLength });
            var trainingParemeters = TrainingParameters.UnitTestDefault;

            var runner = new NetworkInstanceRunner(networkInstance, networkData, trainingParemeters, null, (index, value) =>
            {
                if (index == 1 && !WithinRange(value, .12569589, .0001))
                    Assert.Fail();
                if (index == 18000 && !WithinRange(value, .08327105, .0000001))
                    Assert.Fail();
                if (index == 20000 && !WithinRange(value, .07856123, .0000001))
                    Assert.Fail();
                if (index == 40000 && !WithinRange(value, .07466515, .0000001)) 
                    Assert.Fail();
                if (index == 100000 && !WithinRange(value, .0742145, .0000001))
                    Assert.Fail();


            
                if (index == 1 && WithinRange(value, .12569589, .0001)) 
                { 
                }                
                if (index == 18000 && WithinRange(value, .08327105, .0000001))
                {
                }
                if (index == 20000 && WithinRange(value, .07856123, .0000001))
                {
                }
                if (index == 40000 && WithinRange(value, .07466515, .0000001))
                {
                }
                if (index == 100000 && WithinRange(value, .0742145, .0000001))
                {
                }

            });
            runner.Run();

            while (!runner.IsFinished || runner.HasEvents) 
            {
                runner.ProcessEvents();
            }
        }
    }
}