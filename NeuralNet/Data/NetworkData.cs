using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Data
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
            var lines = File.ReadAllLines(filePath);
            var headers = lines[0];

            Inputs = new double[lines.Length - 1][];
            Outputs = new double[lines.Length - 1][];

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var splitLine = line.Split(',');

                Inputs[i - 1] = new double[splitLine.Length - 1];
                Outputs[i - 1] = new double[10]; // One-hot array for digits 0-9

                for (int j = 1; j < splitLine.Length; j++)
                    Inputs[i - 1][j - 1] = double.Parse(splitLine[j]);

                int label = int.Parse(splitLine[0]);
                Outputs[i - 1][label] = 1.0; // Set the corresponding digit position to 1
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

        public static NetworkData MNISTData { get; } = new NetworkData(@"D:\Data\mnist_train.csv");
    }
}
