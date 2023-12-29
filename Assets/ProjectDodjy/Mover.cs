using UnityEngine;

namespace ProjectDodjy
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 3;
 
        // Update is called once per frame
        void Update()
        {
            movePlayer();
        }

        void movePlayer()
        {
            Vector3 moveTo = new(
                Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed,
                0,
                Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);

            transform.Translate(moveTo);
        }
    }
}
