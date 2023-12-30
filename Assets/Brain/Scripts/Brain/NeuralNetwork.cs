using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeuralNetwork {
    public List<Neuron> Neurons { get; private set; }
    public int SignalPasses { get; private set; }

    public NeuralNetwork(int numNeurons, int minConnections, int maxConnections, int signalPasses) {
        Neurons = new List<Neuron>();
        SignalPasses = signalPasses;
        InitializeNetwork(numNeurons, minConnections, maxConnections);
    }

    private void InitializeNetwork(int numNeurons, int minConnections, int maxConnections) {
        // Create Neurons
        for (int i = 0; i < numNeurons; i++) {
            Neurons.Add(new Neuron());
        }

        // Create Random Connections
        var rnd = new System.Random();
        foreach (var neuron in Neurons) {
            int numConnections = rnd.Next(minConnections, maxConnections + 1);
            for (int i = 0; i < numConnections; i++) {
                var targetNeuron = Neurons[rnd.Next(Neurons.Count)];
                var weight = (float)rnd.NextDouble() * 2 - 1; // Random weight between -1 and 1
                neuron.Connections.Add(new Connection(neuron, targetNeuron, weight));
            }
        }
    }

    public void UpdateNetwork() {
        for (int i = 0; i < SignalPasses; i++) {
            foreach (var neuron in Neurons) {
                neuron.CalculateOutput();
            }
        }
    }



    public IEnumerable<float> GetOutputs(int from_neuron, int to_neuron) {
        // Validate the range
        if (from_neuron < 0 || to_neuron >= Neurons.Count || from_neuron > to_neuron) {
            Debug.Log("Invalid neuron range specified.");
        }

        for (int i = from_neuron; i <= to_neuron; i++) {
            yield return Neurons[i].OutputValue;
        }
    }

    public IEnumerable<Neuron> GetNeurons(int from_neuron, int to_neuron)
    {
        // Validate the range
        if (from_neuron < 0 || to_neuron >= Neurons.Count || from_neuron > to_neuron) {
            Debug.Log("Invalid neuron range specified.");
        }

        for (int i = from_neuron; i <= to_neuron; i++) {
            yield return Neurons[i];
        }
    }

    public void SetNeuronValue(int neuronIndex, float val){
        Neurons[neuronIndex].SetOutputValue(val);
    }
}
