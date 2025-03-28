using NeuralNet.Core.Global;
using NeuralNet.Core.Training;
using NeuralNet.Data;
using NeuralNet.Networks.RonNet.RonBP;

namespace NeuralNet.Tests
{
    [TestClass]
    public class StandardTests1
    {
        public static TrainingParameters UnitTestDefault => new TrainingParameters()
        {
            LearningRate = 0.1,
            Momentum = 0.1,
            ErrorThreshold = 0.01,
            MaxIterations = 100000,
            Verbose = true,
            VerboseModulus = 10
        };

        public static bool WithinRange(double value, double target, double range) => (value >= target - range && value <= target + range);

        [TestMethod]
        public void Test_RonBP_XOR_NonRandomInit()
        {
            var networkData = NetworkData.InitXORData();
            var networkInstance = new RonBPInstance(new int[] { networkData.InputSetLength, 3, networkData.OutputSetLength }, false);
            var trainingParemeters = UnitTestDefault;
            bool isTestFinished = false;

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

                if (index == 100000)
                    isTestFinished = true;

                //if (index == 1 && WithinRange(value, .12569589, .0001)) 
                //{ 
                //}                
                //if (index == 18000 && WithinRange(value, .08327105, .0000001))
                //{
                //}
                //if (index == 20000 && WithinRange(value, .07856123, .0000001))
                //{
                //}
                //if (index == 40000 && WithinRange(value, .07466515, .0000001))
                //{
                //}
                //if (index == 100000 && WithinRange(value, .0742145, .0000001))
                //{
                //}

            });
            runner.Run();

            while (!isTestFinished || !runner.IsFinished || runner.HasEvents) 
            {
                runner.ProcessEvents();
            }

            Assert.IsTrue(isTestFinished);
        }
    }
}