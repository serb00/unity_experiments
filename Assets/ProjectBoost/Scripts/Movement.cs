using UnityEngine;

public class Movement : MonoBehaviour
{
    const float deadzone = 0.01f;
    public int thrust = 800;

    public float angularSpeed = 50;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

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

    private void ApplyRotation(Vector3 vector)
    {
        rb.freezeRotation = true;
        transform.Rotate(vector, angularSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
    }
}
