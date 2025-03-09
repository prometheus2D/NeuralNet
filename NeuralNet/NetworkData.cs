using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet
{
    public class NetworkData
    {
        public double[][] Inputs { get; }
        public double[][] Outputs { get; }

        public NetworkData(double[][] inputs, double[][] outputs)
        {
            if (inputs == null || outputs == null)
                throw new ArgumentNullException("Inputs and Outputs cannot be null.");
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("The number of input samples must match the number of output samples.");

            Inputs = inputs;
            Outputs = outputs;
        }

        public NetworkData(string filePath)
        {
            filePath = @"D:\Data\mnist_train.csv";

            var lines = File.ReadAllLines(filePath);
            var headers = lines[0];

            for (int i = 1; i < lines.Length; i++)
            {

            }




        }


        //Static Data
        public static NetworkData XORData { get; } = new NetworkData(
            new double[][]
            {
                new double[] { 0, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 }
            },
            new double[][]
            {
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 1 },
                new double[] { 0 }
            }
        );

        public static NetworkData MNISTData { get; } = new NetworkData("");
    }
}
