using NeuralNet.Core.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Data
{
    public class NetworkData
    {
        public string Key { get; set; }
        public double[][] Inputs { get; }
        public double[][] Outputs { get; }
        public int InputSetLength => Inputs[0].Length;
        public int OutputSetLength => Outputs[0].Length;
        public NetworkData(string key, double[][] inputs, double[][] outputs)
        {
            Key = key;
            if (inputs == null || outputs == null)
                throw new ArgumentNullException("Inputs and Outputs cannot be null.");
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("The number of input samples must match the number of output samples.");

            Inputs = inputs;
            Outputs = outputs;
        }

        public NetworkData ReduceByOutput(Func<double[], bool> reduceByOutputFilter)
        {
            var newInputs = new List<double[]>();
            var newOutputs = new List<double[]>();

            for (int i = 0; i < Inputs.Length; i++)
            {
                if (reduceByOutputFilter(Outputs[i]))
                {
                    newInputs.Add(Inputs[i]);
                    newOutputs.Add(Outputs[i]);
                }
            }

            return new NetworkData(Key, newInputs.ToArray(), newOutputs.ToArray());
        }
        public NetworkData ShrinkByOutput(int maxOutputCategoryCount)
        {
            var newInputs = new List<double[]>();
            var newOutputs = new List<double[]>();

            var outputCategories = Outputs.Select(x => GetOutputCategory(x)).Distinct().ToList();
            var outputCategoryCounts = new int[outputCategories.Count];

            if (outputCategories.Count > 10)
                throw new NotImplementedException();

            for (int i = 0; i < Outputs.Length; i++)
            {
                var currentOutput = Outputs[i];
                var outputCategoryIndex = outputCategories.IndexOf(GetOutputCategory(currentOutput));


                if (outputCategoryCounts[outputCategoryIndex] < maxOutputCategoryCount)
                {
                    newInputs.Add(Inputs[i]);
                    newOutputs.Add(currentOutput);
                    outputCategoryCounts[outputCategoryIndex]++;
                }
            }

            return new NetworkData(Key, newInputs.ToArray(), newOutputs.ToArray());

            string GetOutputCategory(double[] output)
            {
                var result = "";
                for (int i = 0; i < output.Length; i++)
                {
                    result += output[i].ToString();
                }
                return result;
            }
        }

        public NetworkData(string key, string filePath)
        {
            Key = key;
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
        public static NetworkData GetNetworkData(string key)
        {
            if (_NetworkDataDictionary.ContainsKey(key))
                return _NetworkDataDictionary[key];

            var newData = NetworkDataFactoryDictionary[key]();
            _NetworkDataDictionary.Add(newData.Key, newData);
            return newData;
        }
        private static Dictionary<string, NetworkData> _NetworkDataDictionary { get; set; } = new Dictionary<string, NetworkData>();
        public static Dictionary<string, Func<NetworkData>> NetworkDataFactoryDictionary = 
            new Dictionary<string, Func<NetworkData>>()
            {
                { "XOR", () => InitXORData() },
                { "MNIST", () => InitMNISTData() },
                { "MNIST-small", () => InitMNISTData("MNIST-small")
                    .ReduceByOutput(output =>
                    {
                        return output[0] == 1 || output[1] == 1;// || output[2] == 1 || output[3] == 1;
                    })
                    .ShrinkByOutput(200)}
            };
        public static NetworkData InitXORData() => new NetworkData("XOR",
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

        public static NetworkData InitMNISTData(string keyOverride = null) => new NetworkData(keyOverride ?? "MNIST", @"D:\Data\mnist_train.csv");
    }
}
