﻿using System;

namespace NeuralNet.Core.Training
{
    /// <summary>
    /// Contains final training results.
    /// </summary>
    public class TrainingResult
    {
        public int IterationCount { get; set; }
        public double FinalError { get; set; }
        public TimeSpan TrainingTime { get; set; }
    }
}
