using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    int hits = 0;
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Hit"))
        {
            hits++;
            Debug.Log("You hit obsticles " + hits + " times.");
        }
    }
}
