using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NeuralNetworkVisualizer : MonoBehaviour {
    
#region Attributes
    public GameObject NeuronPrefab;
    public GameObject ConnectionPrefab;
    public GameObject SelfConnectionPrefab;
    public Brain BrainInstance;

    public float connectionLenghtOffset = 1f;

    private float Margin;
    public float InputOutputSpacing;

    private RectTransform rectTransform;
    private float panelWidth;
    private float panelHeight;
    private Vector3 panelOffset;

    private List<GameObject> neuronGameObjects;
    private List<GameObject> connectionGameObjects;

    private readonly Color colorMin = Color.red;
    private readonly Color colorMid = Color.white;
    private readonly Color colorMax = Color.green;

#endregion Attributes

    void Start() {
        if (BrainInstance == null) {
            // Create a default Brain
            BrainInstance = new Brain(20, 4, 5, 2, 40, 1);
        }

        
        neuronGameObjects = new List<GameObject>(BrainInstance.NeuronsCount());
        connectionGameObjects = new List<GameObject>(BrainInstance.ConnectionsCount());
        
        rectTransform = GetComponent<RectTransform>();
        panelWidth = rectTransform.rect.width;
        panelHeight = rectTransform.rect.height;
        panelOffset = new Vector3(panelWidth / 2, panelHeight / 2, 0);
        Margin = panelHeight / 20;
        
        CreateGraph();
    }

    void Update() {
        BrainInstance.UpdateBrain();
        UpdateGraph();
    }

    private void CreateGraph() {
        // Access the neurons from the Brain's NeuralNetwork
        foreach (var neuron in BrainInstance.GetAllNeurons()) {
            NeuronType type = DetermineNeuronType(neuron.ID);
            var neuronGO = Instantiate(NeuronPrefab, rectTransform);
            neuronGO.transform.localPosition = GetNeuronPosition(neuron.ID, type) - panelOffset;
            neuronGO.name = string.Format("Neuron {0} - {1}", neuron.ID, type);
            neuronGameObjects.Insert(neuron.ID, neuronGO);
        }

        // Instantiate and setup connections
        foreach (var neuron in BrainInstance.GetAllNeurons()) {
            foreach (var connection in neuron.Connections) {
                if (connection.SourceNeuron == connection.TargetNeuron) {
                    // Create a self-connection
                    CreateSelfConnection(neuron, connection.Weight);
                } else {
                    // Create a linear connection
                    CreateLinearConnection(connection);
                }
            }
        }
    }

#region DrawingConnections

    private void CreateLinearConnection(Connection connection) {
        var sourcePos = neuronGameObjects[connection.SourceNeuron.ID].transform.localPosition;
        var targetPos = neuronGameObjects[connection.TargetNeuron.ID].transform.localPosition;

        var connectionGO = Instantiate(ConnectionPrefab, rectTransform);
        connectionGO.name = $"Connection {connection.SourceNeuron.ID} -> {connection.TargetNeuron.ID}";
        var image = connectionGO.GetComponent<Image>(); 
        if (connection.Weight < 0) {
            // If weight is between -1 and 0, interpolate between red and white
            image.color = Color.Lerp(colorMin, colorMid, connection.Weight + 1);
        } else {
            // If weight is between 0 and 1, interpolate between white and green
            image.color = Color.Lerp(colorMid, colorMax, connection.Weight);
        }

        // Position
        var midpoint = (sourcePos + targetPos) / 2;
        connectionGO.transform.localPosition = midpoint;

        // Rotation
        var direction = targetPos - sourcePos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        connectionGO.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Scale
        RectTransform connectionRectTransform = connectionGO.GetComponent<RectTransform>();
        float length = Vector3.Distance(sourcePos, targetPos);
        connectionRectTransform.sizeDelta = new Vector2(length - connectionLenghtOffset, connectionRectTransform.sizeDelta.y); // Adjust y for thickness based on weight

        connectionGameObjects.Add(connectionGO);
    }

    private void CreateSelfConnection(Neuron neuron, float weight) {
        var neuronPos = neuronGameObjects[neuron.ID].transform.localPosition;
        
        var selfConnectionGO = Instantiate(SelfConnectionPrefab, rectTransform);
        selfConnectionGO.name = string.Format("Self Connection {0}", neuron.ID);
        var image = selfConnectionGO.GetComponent<Image>(); 
        if (weight < 0) {
            // If weight is between -1 and 0, interpolate between red and white
            image.color = Color.Lerp(colorMin, colorMid, weight + 1);
        } else {
            // If weight is between 0 and 1, interpolate between white and green
            image.color = Color.Lerp(colorMid, colorMax, weight);
        }
        // Position
        Vector3 offset = new Vector3(0, 10, 0);
        selfConnectionGO.transform.localPosition = neuronPos + offset;


        connectionGameObjects.Add(selfConnectionGO);
    }

#endregion DrawingConnections

#region DrawingNeurons
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
        //Debug.Log($"Neuron {index} position: {x}, {y}");
        return new Vector3(x, y, 0);
    }

    private float CalculateVerticalPosition(int index, int totalNeuronsOfType, float panelHeight) {
        float spacing = (panelHeight - 2 * Margin) / (totalNeuronsOfType - 1);
        return Margin + spacing * index;
    }

    private void UpdateNeuronColor(GameObject neuronGO, float outputValue) {
        var image = neuronGO.GetComponent<Image>();
        // Set the color based on the output value: red-white-green gradient
        if (outputValue < 0) {
            // If outputValue is between -1 and 0, interpolate between red and white
            image.color = Color.Lerp(colorMin, colorMid, outputValue + 1);
        } else {
            // If outputValue is between 0 and 1, interpolate between white and green
            image.color = Color.Lerp(colorMid, colorMax, outputValue);
        }
    }

#endregion DrawingNeurons

    private void UpdateGraph() {
        // Update neurons' colors based on their output values
        for (int i = 0; i < BrainInstance.NeuronsCount(); i++) {
            UpdateNeuronColor(neuronGameObjects[i], BrainInstance.GetNeuron(i));
        }
    }

}

