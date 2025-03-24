namespace NeuralNet.Core.Training
{
    /// <summary>
    /// Contains training progress information.
    /// </summary>
    public class TrainingProgress
    {
        public int Iteration { get; set; }
        public double Error { get; set; }
    }
}
