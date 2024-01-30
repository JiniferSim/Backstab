using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Transform player;
    public LayerMask obstacleLayer;
    public bool seen = false;

    private Color baseColor;
    private Renderer baseRenderer;
    [SerializeField] private Color watcherColor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseRenderer = GetComponent<Renderer>();
        baseColor = baseRenderer.material.color;
    }
    void Update()
    {
        seen = IsPlayerInSight();
        Backsatab.Instance.IsPlayerVisibleToAnyEnemy(this, seen);

        baseRenderer.material.color = seen ? watcherColor : baseColor;

    }

    public bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        RaycastHit hit;


        float dotProductToPlayer = Vector3.Dot(transform.forward, directionToPlayer.normalized);

        if (dotProductToPlayer > 0.6f) // Adjust the threshold angle as needed
        {
            Debug.DrawRay(transform.position, (directionToPlayer) * 100);
            if (Physics.Raycast(transform.position, directionToPlayer, out hit,1000, obstacleLayer))
            {
                Debug.Log("I hit " + hit.collider.name);
                if (hit.collider.CompareTag("Player"))
                {
                    return true; // Player is within sight
                }
            }
            else return false;
        }
            
        

        return false;
    }
}
