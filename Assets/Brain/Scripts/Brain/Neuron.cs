using System.Collections.Generic;
using UnityEngine;

public class Neuron {
    public List<Connection> Connections { get; set; }
    public float OutputValue { get; private set; }

    public Neuron() {
        Connections = new List<Connection>();
    }

    public void CalculateOutput() {
        OutputValue = 0;
        foreach (var conn in Connections) {
            OutputValue += conn.SourceNeuron.OutputValue * conn.Weight;
        }
        OutputValue = Mathf.Clamp(OutputValue, -1, 1); // Assuming a range of -1 to 1 for the output
    }

    public void SetOutputValue(float val) {
        OutputValue = val;
    }
}
