using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Backsatab : MonoBehaviour
{
    public static Backsatab Instance;
    public TMP_Text textMeshPro;
    public Animator animator;
    public Transform playerTransform;
    public Transform enemyTransform;

    public Canvas canvas;
    public Image iconImage;
    public Sprite OpenedEye;
    public Sprite ClosedEye;

    private bool isShowingText = false;

    private List<EnemyView> watchers = new List<EnemyView>();

    void Start()
    {
        Instance = this;
        //Show text if enemy doesn't see player, if player looks directly at the enemy, player is behind enemy
    }

    void Update()
    {
        if (!isShowingText && IsPlayerBehindEnemy() && watchers.Count == 0)
        {
            // Enable the text animation
            textMeshPro.enabled = true;
            animator.Play("text_anim");
            StartCoroutine(HideTextAfterDelay(2f));
        }

        UpdateCanvasIcon();
    }

    private bool IsPlayerBehindEnemy()
    {
        Vector3 playerToEnemy = enemyTransform.position - playerTransform.position;
        float angle = Vector3.Angle(playerTransform.forward, playerToEnemy);

        return Mathf.Abs(angle) < 90f;
    }

    public void IsPlayerVisible(EnemyView enemy, bool visible)
    {
       if(visible && !watchers.Contains(enemy))
        {
            watchers.Add(enemy);
        } else if (!visible && watchers.Contains(enemy))
        {
            watchers.Remove(enemy);
        }
        // Implement logic to check if the enemy sees the player
        // You might use raycasting or other techniques to determine line of sight
        // Return true if the player is in sight, false otherwise
        //return false;
    }

    void UpdateCanvasIcon()
    {
        if (canvas != null && iconImage != null)
        {
            if (watchers.Count > 0)
            {
                iconImage.sprite = OpenedEye;
            }
            else
            {
                iconImage.sprite = ClosedEye;
            }
        }

    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        isShowingText = true;
        yield return new WaitForSeconds(delay);
        isShowingText = false;
        animator.StopPlayback();
    }
}
