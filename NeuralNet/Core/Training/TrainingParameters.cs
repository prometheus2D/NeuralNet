namespace NeuralNet.Core.Training
{
    /// <summary>
    /// Encapsulates training parameters for a network.
    /// </summary>
    public class TrainingParameters
    {
        public double LearningRate { get; set; } = 0.1;
        public double Momentum { get; set; } = 0.0;
        public double ErrorThreshold { get; set; } = 0.01;
        public int MaxIterations { get; set; } = 100000;
        public bool Verbose { get; set; } = true;
        public int VerboseModulus { get; set; } = 10;
    }
}
