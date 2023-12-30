using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NeuralNetworkVisualizer : MonoBehaviour {
    public GameObject NeuronPrefab;
    public GameObject ConnectionPrefab;
    public Brain BrainInstance;

    public float Margin;
    public float InputOutputSpacing;

    private RectTransform rectTransform;
    private float panelWidth;
    private float panelHeight;
    private Vector3 panelOffset;

    private List<GameObject> neuronGameObjects;
    private List<GameObject> connectionGameObjects;

    void Start() {
        if (BrainInstance == null) {
            // Create a default Brain
            BrainInstance = new Brain(40, 8, 6, 0, 10, 5);
        }

        neuronGameObjects = new List<GameObject>();
        connectionGameObjects = new List<GameObject>();
        rectTransform = GetComponent<RectTransform>();
        panelWidth = rectTransform.rect.width;
        panelHeight = rectTransform.rect.height;
        panelOffset = new Vector3(panelWidth / 2, panelHeight / 2, 0);
        Margin = panelHeight / 20;
        CreateGraph();
    }

    void Update() {
        UpdateGraph();
    }

    // ... Rest of the implementation ...

    private void CreateGraph() {
        // Access the neurons from the Brain's NeuralNetwork
        int index = 0;
        foreach (var neuron in BrainInstance.GetAllNeurons()) {
            NeuronType type = DetermineNeuronType(index);
            var neuronGO = Instantiate(NeuronPrefab, rectTransform);
            neuronGO.transform.localPosition = GetNeuronPosition(index, type) - panelOffset;
            neuronGO.name = string.Format("Neuron {0} - {1}", index, type);
            neuronGameObjects.Add(neuronGO);
            index++;
        }

        // Instantiate and setup connections
        // ... Similar logic to previous implementation, but access connections from Brain's NeuralNetwork
    }

    private NeuronType DetermineNeuronType(int index) {
        if (index < BrainInstance.numInputNeurons) {
            return NeuronType.Input;
        } else if (index >= BrainInstance.NeuronsCount() - BrainInstance.numOutputNeurons) {
            return NeuronType.Output;
        } else {
            return NeuronType.Hidden;
        }
    }

    private Vector3 GetNeuronPosition(int index, NeuronType type) {
        float x, y;
        switch (type) {
            case NeuronType.Input:
                x = Margin;
                y = CalculateVerticalPosition(index, BrainInstance.numInputNeurons, panelHeight);
                break;
            case NeuronType.Output:
                x = panelWidth - Margin;
                y = CalculateVerticalPosition(index - BrainInstance.NeuronsCount() + BrainInstance.numOutputNeurons, BrainInstance.numOutputNeurons, panelHeight);
                break;
            case NeuronType.Hidden:
                x = Random.Range(Margin + InputOutputSpacing, panelWidth - Margin - InputOutputSpacing);
                y = Random.Range(Margin, panelHeight - Margin);
                break;
            default:
                x = 0; y = 0;
                Debug.Log((nameof(type), "Invalid neuron type."));
                break;
        }
        Debug.Log($"Neuron {index} position: {x}, {y}");
        return new Vector3(x, y, 0);
    }

    private float CalculateVerticalPosition(int index, int totalNeuronsOfType, float panelHeight) {
        float spacing = (panelHeight - 2 * Margin) / (totalNeuronsOfType - 1);
        return Margin + spacing * index;
    }


    private void UpdateGraph() {
        // Update neurons' colors based on their output values
        for (int i = 0; i < BrainInstance.NeuronsCount(); i++) {
            UpdateNeuronColor(neuronGameObjects[i], BrainInstance.GetNeuron(i));
        }

        // Optionally update connections if their visuals need to change (e.g., thickness based on weight)
        // ...
    }

    private void UpdateNeuronColor(GameObject neuronGO, float outputValue) {
        var image = neuronGO.GetComponent<Image>();
        // Set the color based on the output value: red-white-green gradient
        // ...
    }

    // ... Implement other utility methods as before ...
}

