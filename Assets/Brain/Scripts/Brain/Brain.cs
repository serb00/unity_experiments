using System.Collections.Generic;
using System.Linq;

public class Brain {
    private NeuralNetwork neuralNetwork;
    public int numInputNeurons;
    public int numOutputNeurons;
    private Dictionary<int, Sensor> inputMappings;
    private Dictionary<int, Action> outputMappings;

    public Brain(int totalNeurons, int numInputs, int numOutputs, int minConnections, int maxConnections, int signalPasses) {
        numInputNeurons = numInputs;
        numOutputNeurons = numOutputs;
        neuralNetwork = new NeuralNetwork(totalNeurons, minConnections, maxConnections, signalPasses);
        inputMappings = new Dictionary<int, Sensor>();
        outputMappings = new Dictionary<int, Action>();
        InitializeMappings();
    }

    private void InitializeMappings() {
        // Initialize inputMappings and outputMappings
        // Example: inputMappings[0] = new Sensor(...);
        // Example: outputMappings[0] = new Action(...);
    }

    public void UpdateBrain() {
        // Update input neurons based on sensor data
        for (int i = 0; i < numInputNeurons; i++) {
            neuralNetwork.SetNeuronValue(i, inputMappings[i].GetValue());
        }

        // Update the neural network
        neuralNetwork.UpdateNetwork();

        // Execute actions based on output neurons
        for (int i = 0; i < numOutputNeurons; i++) {
            int outputIndex = neuralNetwork.Neurons.Count - numOutputNeurons + i;
            outputMappings[i].Execute(neuralNetwork.Neurons[outputIndex].OutputValue);
        }
    }

    public IEnumerable<Neuron> GetInputNeurons()
    {
        return neuralNetwork.GetNeurons(0, numInputNeurons);
    }

    public IEnumerable<Neuron> GetOutputNeurons() {
        return neuralNetwork.GetNeurons(
            neuralNetwork.Neurons.Count - numOutputNeurons,
            neuralNetwork.Neurons.Count);
    }

    public IEnumerable<Neuron> GetHiddenNeurons()
    {
        return neuralNetwork.GetNeurons(
            numInputNeurons,
            neuralNetwork.Neurons.Count - numOutputNeurons);
    }

    public IEnumerable<Neuron> GetAllNeurons() {
        return neuralNetwork.Neurons;
    }

    public int NeuronsCount(){
        return neuralNetwork.Neurons.Count;
    }

    public float GetNeuron(int index){
        return neuralNetwork.GetOutputs(index, index).First<float>();
    }
}

public enum NeuronType { Input, Output, Hidden }


// Dummy Sensor and Action classes for demonstration purposes
public class Sensor {
    public float GetValue() {
        // Get the sensor value (e.g., from a game object or an external source)
        return 0; // Placeholder
    }
}

public class Action {
    public void Execute(float signal) {
        // Perform the action based on the signal
        // Example: move a game object, change a state, etc.
    }
}
