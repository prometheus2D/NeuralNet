namespace NeuralNet
{
    /// <summary>
    /// Encapsulates training parameters for a network.
    /// </summary>
    public class TrainingParameters
    {
        public double LearningRate { get; set; } = 0.1;
        public double Momentum { get; set; } = 0.0;
        public double ErrorThreshold { get; set; } = 0.01;
        public int MaxIterations { get; set; } = 10000;
        public bool Verbose { get; set; } = false;
        public int VerboseModulus { get; set; } = 0;
    }
}
