using System.Collections.Generic;
using UnityEngine;

public class Neuron {
    public List<Connection> Connections { get; set; }
    public float OutputValue { get; private set; }
    public int ID { get; set; }

    public Neuron(int id) {
        Connections = new List<Connection>();
        ID = id;
        OutputValue = Random.Range(-1f, 1f);
    }

    public void CalculateOutput() {
        float tempValue = 0f;
        foreach (var conn in Connections) {
            tempValue += conn.SourceNeuron.OutputValue * conn.Weight;
        }
        OutputValue = Mathf.Clamp(tempValue, -1, 1); // Assuming a range of -1 to 1 for the output
    }

    public void SetOutputValue(float val) {
        OutputValue = val;
    }
}
