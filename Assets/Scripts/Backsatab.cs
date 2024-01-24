using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Backsatab : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public Animator animator;
    public Transform playerTransform;
    public Transform enemyTransform;

    private bool isShowingText = false;

    void Start()
    {
        //Show text if enemy doesn't see player, if player looks directly at the enemy, player is behind enemy
    }

    void Update()
    {
        if (!isShowingText && IsPlayerBehindEnemy() && !IsPlayerVisible())
        {
            // Enable the text animation
            textMeshPro.enabled = true;
            animator.Play("text_anim");
            StartCoroutine(HideTextAfterDelay(2f));
        }
    }

    private bool IsPlayerBehindEnemy()
    {
        Vector3 playerToEnemy = enemyTransform.position - playerTransform.position;
        float angle = Vector3.Angle(playerTransform.forward, playerToEnemy);

        return Mathf.Abs(angle) < 90f;
    }

    private bool IsPlayerVisible()
    {
        EnemyView enemyView = gameObject.GetComponent<EnemyView>();
        enemyView.IsPlayerInSight();
        // Implement logic to check if the enemy sees the player
        // You might use raycasting or other techniques to determine line of sight
        // Return true if the player is in sight, false otherwise
        return false;
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        isShowingText = true;
        yield return new WaitForSeconds(delay);
        isShowingText = false;
        animator.StopPlayback();
    }
}
