namespace NeuralNet.Core.Training
{
    /// <summary>
    /// Encapsulates training parameters for a network.
    /// </summary>
    public class TrainingParameters
    {
        public static TrainingParameters Default => new TrainingParameters()
        {
            LearningRate = 0.1,
            Momentum = 0.1,
            ErrorThreshold = 0.01,
            MaxIterations = 10000,
            Verbose = true,
            VerboseModulus = 10
        };
        public static TrainingParameters UnitTestDefault => new TrainingParameters()
        {
            LearningRate = 0.1,
            Momentum = 0.1,
            ErrorThreshold = 0.01,
            MaxIterations = 100000,
            Verbose = true,
            VerboseModulus = 10
        };


        public double LearningRate { get; set; } = 0.1;
        public double Momentum { get; set; } = 0.1;
        public double ErrorThreshold { get; set; } = 0.01;
        public int MaxIterations { get; set; } = 10000;
        public bool Verbose { get; set; } = true;
        public int VerboseModulus { get; set; } = 10;
    }
}
