using System;
using Accord.Neuro;
using Accord.Neuro.Learning;
using Encog.ML.Data.Basic;
using Encog.ML.Data;
using NeuralNet.Network;
using Encog.Neural.Networks.Training.Propagation.Back;
using System.Diagnostics;

namespace NeuralNet
{
    /// <summary>
    /// A concrete network instance using the Accord.NET library.
    /// </summary>
    public class RonBPInstance : AbstractNetworkInstance
    {
        //Structs
        public struct Neuron
        {
            //Data Fields
            public int NeuronID;
        }
        public struct Synapse
        {
            //Data Fields
            public int SynapseID;
            public int InputNeuronID;
            public int OutputNeuronID;
            public float Weight;
        }

        //Fields
        public Neuron[] Neurons { get; }
        public Synapse[] Synapses { get; }
        public float[] NeuronSums { get; }
        public float[] NeuronOutputs { get; }
        public float[] NeuronErrors { get; }
        public int[] NeuronLayerStructure { get; }

        //Interface Properties
        public IEnumerable<float> OutputErrors
        {
            get
            {
                for (int i = NeuronErrors.Length - NeuronLayerStructure[NeuronLayerStructure.Length - 1]; i < NeuronErrors.Length; i++)
                    yield return NeuronErrors[i];
            }
        }

        //Constructor
        public RonBPInstance(int[] layers)
        {
            var currentRawNeuronID = 0;
            var currentRawSynapseID = 0;
            var neurons = new Neuron[layers.Sum()];
            var synapses = new Synapse[SynapseTotal(layers)];

            var neuronLayerStart = 0;
            var neuronLayerEnd = 0;

            //Iterate Over Layers
            for (int currentLayerIndex = 0; currentLayerIndex < layers.Length; currentLayerIndex++)
            {
                //Set Previous Layer Boundaries
                if (currentLayerIndex > 0)
                {
                    neuronLayerStart = neuronLayerEnd;
                    neuronLayerEnd += layers[currentLayerIndex - 1];
                }

                //Iterate Over Neurons
                var currentLayerCount = layers[currentLayerIndex];
                for (int currentLayerNeuronIndex = 0; currentLayerNeuronIndex < currentLayerCount; currentLayerNeuronIndex++)
                {
                    //Create Neuron
                    var currentNeuron = neurons[currentRawNeuronID] = new Neuron()
                    {
                        NeuronID = currentRawNeuronID
                    };
                    currentRawNeuronID++;

                    //If Base Layer, no Synapses to create
                    if (currentLayerIndex == 0)
                        continue;

                    //Iterate Over Previous layer neurons to create synapses
                    for (int previousRawNeuronIndex = neuronLayerStart; previousRawNeuronIndex < neuronLayerEnd; previousRawNeuronIndex++)
                    {
                        var newSynapse = synapses[currentRawSynapseID] = new Synapse()
                        {
                            SynapseID = currentRawSynapseID,
                            InputNeuronID = previousRawNeuronIndex,
                            OutputNeuronID = currentRawNeuronID - 1
                        };
                        currentRawSynapseID++;
                    }
                }
            }


            NeuronLayerStructure = layers;
            Neurons = neurons.ToArray();
            Synapses = synapses.ToArray();
            NeuronSums = new float[Neurons.Length];
            NeuronOutputs = new float[Neurons.Length];
            NeuronErrors = new float[Neurons.Length];

            int SynapseTotal(int[] _layers)
            {
                var result = 0;
                for (int i = 1; i < _layers.Length; i++)
                    result += _layers[i - 1] * layers[i];
                return result;
            }
        }

        //Actions
        public void TrainOnCases(float[][][] cases, float learningRate)
        {
            foreach (var singleCase in cases)
                TrainOnCase(singleCase, learningRate);
        }
        public void TrainOnCase(float[][] singleCase, float learningRate)
        {
            Iterate(singleCase[0]);
            CalculateOutputErrors(singleCase[1]);
            BackPropagate(learningRate);
        }
        public float TestOnCases(float[][][] cases)
        {
            var sum = 0f;
            foreach (var singleCase in cases)
                sum += TestOnCase(singleCase);
            return sum;
        }
        public float TestOnCase(float[][] singleCase)
        {
            Iterate(singleCase[0]);
            CalculateOutputErrors(singleCase[1]);
            return OutputErrors.Sum();
        }

        //Operations
        public void CalculateOutputErrors(float[] expectedOutputs)
        {
            // Calculate errors for the output layer
            int outputStartIndex = Neurons.Length - NeuronLayerStructure.Last();
            for (int i = 0; i < NeuronLayerStructure.Last(); i++)
            {
                int neuronIndex = outputStartIndex + i;
                float output = NeuronOutputs[neuronIndex];
                NeuronErrors[neuronIndex] = (expectedOutputs[i] - output) * SigmoidDerivative(output);
            }
        }
        public void Iterate(float[] inputs)
        {
            //Clean
            for (int i = 0; i < NeuronSums.Length; i++)
                NeuronSums[i] = 0;

            //Initialize input layer neuron outputs with the input values
            for (int i = 0; i < inputs.Length; i++)
                NeuronOutputs[i] = inputs[i]; // Input neurons take values directly from inputs

            //Iterate over layers and calculate synapse inputs to outputs
            var synapseIndex = 0;
            var inputNeuronIndex = 0;
            var outputStartIndex = 0;
            for (int i = 1; i < NeuronLayerStructure.Length; i++)
            {
                //Setup Loop
                var synapseStartIndex = synapseIndex;
                synapseIndex += NeuronLayerStructure[i - 1] * NeuronLayerStructure[i];
                var synapseEndIndex = synapseIndex;

                var inputNeuronStartIndex = inputNeuronIndex;
                inputNeuronIndex += NeuronLayerStructure[i - 1];
                var inputNeuronEndIndex = inputNeuronIndex;
                var outputNeuronEndIndex = inputNeuronEndIndex + NeuronLayerStructure[i];

                var lastLayer = i == NeuronLayerStructure.Length - 1;

                //Create Layer Sums
                for (int j = synapseStartIndex; j < synapseEndIndex; j++)
                {
                    var synapse = Synapses[j];
                    NeuronSums[synapse.OutputNeuronID] += NeuronOutputs[synapse.InputNeuronID] * synapse.Weight;
                }

                //Create Layer Outputs ??CAHNGE TO OUTPUT NEURONS NOT INPUT
                for (int j = inputNeuronEndIndex; j < outputNeuronEndIndex; j++)
                    NeuronOutputs[j] = Sigmoid(NeuronSums[j]);

                if (lastLayer)
                    outputStartIndex = inputNeuronEndIndex;
            }

            //Calculate Final Layer and Yield
            for (int i = outputStartIndex; i < Neurons.Length; i++)
                NeuronOutputs[i] = Sigmoid(NeuronSums[i]);
        }
        public void BackPropagate(float learningRate)
        {
            // Propagate errors backward from the output to the input layer
            int synapseIndex = Synapses.Length; // Start from the last synapse
            for (int layer = NeuronLayerStructure.Length - 2; layer >= 0; layer--)
            {
                int layerStartIndex = layer > 0 ? NeuronLayerStructure.Take(layer).Sum() : 0;
                int layerEndIndex = layerStartIndex + NeuronLayerStructure[layer];
                //int nextLayerStartIndex = layerEndIndex;
                //int nextLayerEndIndex = nextLayerStartIndex + NeuronLayerStructure[layer + 1];

                // Update synapses between current layer and next layer
                synapseIndex -= NeuronLayerStructure[layer] * NeuronLayerStructure[layer + 1];
                int synapseStartIndex = synapseIndex;

                // Compute the delta for each neuron in the current layer
                for (int i = layerStartIndex; i < layerEndIndex; i++)
                {
                    float delta = 0.0f;
                    for (int synIndex = synapseStartIndex; synIndex < synapseStartIndex + NeuronLayerStructure[layer] * NeuronLayerStructure[layer + 1]; synIndex++)
                    {
                        if (Synapses[synIndex].InputNeuronID == i)
                        {
                            delta += Synapses[synIndex].Weight * NeuronErrors[Synapses[synIndex].OutputNeuronID];
                        }
                    }
                    NeuronErrors[i] = delta * SigmoidDerivative(NeuronOutputs[i]);
                }

                //Apply Changes to synapses
                for (int synIndex = synapseStartIndex; synIndex < synapseStartIndex + NeuronLayerStructure[layer] * NeuronLayerStructure[layer + 1]; synIndex++)
                {
                    Synapses[synIndex].Weight += learningRate * NeuronOutputs[Synapses[synIndex].InputNeuronID] * NeuronErrors[Synapses[synIndex].OutputNeuronID];
                }
            }
        }

        //Helper Functions
        private float Sigmoid(float x, float beta = 1f)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-beta * x));
        }
        private float SigmoidDerivative(float output, float beta = 1f)
        {
            return beta * output * (1 - output);
        }

        //Interface Functions

        public override TrainingResult Train(NetworkData data, TrainingParameters parameters, Action<TrainingProgress> progressCallback)
        {
            return null;
        }

        public override double[][] Test(NetworkData data)
        {
            return null;
        }
    }
}

