using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Transform player;
    public float sightRange = 5f;
    public LayerMask obstacleLayer;

    public Canvas canvas;
    public Image iconImage;
    public Sprite OpenedEye;
    public Sprite ClosedEye;

    private bool isPlayerInSight = false;

    void Update()
    {
        if (IsPlayerInSight())
        {
            Debug.Log("Player is in sight!");
            isPlayerInSight = true;
        }
        else
        {
            isPlayerInSight = false;
        }
        UpdateCanvasIcon();
    }

    bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange, ~obstacleLayer))
        {
            // Check if the hit object is the player
            if (hit.collider.CompareTag("Player"))
            {
                // Check if the player is within the field of view
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer.normalized);
                if (angleToPlayer < 90f)
                {
                    return true; // Player is within sight
                }
            }
        }
        return false;
    }

    void UpdateCanvasIcon()
    {
        if (canvas != null && iconImage != null)
        {
            if (isPlayerInSight)
            {
                iconImage.sprite = OpenedEye;
            }
            else
            {
                iconImage.sprite = ClosedEye;
            }
        }

    }
}
