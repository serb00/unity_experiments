using UnityEngine;

public class FallTimer : MonoBehaviour
{
    /// <summary>
    /// The time limit in seconds before gravity is turned on.
    /// </summary>
    public int timeLimit = 3;

    /// <summary>
    /// Indicates whether the game object has fallen or not.
    /// </summary>
    private bool falled = false;

    MeshRenderer meshRenderer;
    Rigidbody rigidBody;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer.enabled = false;
        rigidBody.useGravity = false;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        // Turn on gravity if timer has reached the time limit.
        if (Time.time > timeLimit && !falled) {
            falled = true;
            meshRenderer.enabled = true;
            rigidBody.useGravity = true;
            Debug.Log("Falling");
        }
    }
}
