using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirections : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float changeDirectionInterval = 3f;
    private float directionChangeTimer;

    private Quaternion targetRotation;

    void Update()
    {
        directionChangeTimer -= Time.deltaTime;

        // Change rotation direction when the timer reaches zero
        if (directionChangeTimer <= 0f)
        {
            float randomYaw = Random.Range(-180f, 180f);
            targetRotation = Quaternion.Euler(0f, randomYaw, 0f);
            directionChangeTimer = changeDirectionInterval;
        }

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
