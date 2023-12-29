using UnityEngine;

/// <summary>
/// Handles movement and rotation logic for a spaceship controller.
/// Uses input and physics to control thrust and rotation.
/// </summary>
public class Movement : MonoBehaviour
{
    const float deadzone = 0.01f;
    public int thrust = 800;

    public float angularSpeed = 50;
    Rigidbody rb;
    AudioSource audioSource;


    /// <summary>
    /// Initializes the Rigidbody and AudioSource fields.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    /// <summary>
    /// Calls the ProcessThrust() and ProcessRotation() methods.
    /// </summary>
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    /// <summary>
    /// Processes the rotation of the spaceship based on the horizontal input axis.
    /// </summary>
    private void ProcessRotation()
    {

        var val = Input.GetAxis("Horizontal");
        if (val < -deadzone)
        {
            ApplyRotation(Vector3.forward);
        }
        else if (val > deadzone)
        {
            ApplyRotation(Vector3.back);
        }
    }

    /// <summary>
    /// Applies rotation to the spaceship using the given vector and angular speed.
    /// </summary>
    /// <param name="vector">The vector representing the rotation direction.</param>
    private void ApplyRotation(Vector3 vector)
    {
        rb.freezeRotation = true;
        transform.Rotate(vector, angularSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }

    /// <summary>
    /// Processes the thrust of the spaceship based on the space key input.
    /// </summary>
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying) audioSource.Play();
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
