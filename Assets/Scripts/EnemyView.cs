using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Transform player;
    public LayerMask obstacleLayer;

    private Color baseColor;
    private Renderer baseRenderer;
    [SerializeField] private Color watcherColor;

    private bool isPlayerInSight = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseRenderer = GetComponent<Renderer>();
        baseColor = baseRenderer.material.color;
    }
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
    }

    public bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, obstacleLayer))
        {
            // Check if the hit object is the player
            if (hit.collider.CompareTag("Player"))
            {
                // Check if the player is within the field of view
                float dotProductToPlayer = Vector3.Dot(transform.forward, directionToPlayer.normalized);
                if (dotProductToPlayer >= 0)
                {
                    Backsatab.Instance.IsPlayerVisible(this, true);
                    baseRenderer.material.color = watcherColor;
                    return true; // Player is within sight
                }
            }
        }
        Backsatab.Instance.IsPlayerVisible(this, false);
        baseRenderer.material.color = baseColor;
        return false;
    }
}
