using UnityEngine;
using System.Collections;

public class Directions : MonoBehaviour
{
    public float rotateSpeed;
    public float forwardSpeed;
    private Directions directions;

    // Use this for initialization
    void Start()
    {
        directions = GetComponent<Directions>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float speed = forwardSpeed * Input.GetAxis("Vertical");
        directions.SimpleMove(speed * forward);
    }

    public void SimpleMove(Vector3 direction)
    {
        // Normalize the direction vector to ensure consistent movement speed in all directions
        direction.Normalize();

        // Calculate the movement vector
        Vector3 movement = direction * forwardSpeed * Time.deltaTime;

        // Move the GameObject
        transform.Translate(movement, Space.World);
    }
}
