using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    /// <summary>
    /// Handles collisions in a Unity game. Determines the tag of the collided object and performs actions based on the tag.
    /// </summary>
    /// <param name="other">The Collision object containing information about the collision.</param>
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                // Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finished. Next level!");
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                int nextLevelIndex = (currentSceneIndex + 1) % SceneManager.sceneCount;
                SceneManager.LoadScene(nextLevelIndex);
                break;
            default:
                ReloadScene();
                break;
        }
    }

    void ReloadScene(){
        Debug.Log("Failed. Reloading scene.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}