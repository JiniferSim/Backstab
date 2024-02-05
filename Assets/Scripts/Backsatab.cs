using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Backsatab : EnemyView
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
    public bool seen = false;

    [SerializeField] private List<EnemyView> watchers = new List<EnemyView>();

    void Start()
    {
        animator = GetComponent<Animator>();
        Instance = this;
        if (textMeshPro != null)
        {
            textMeshPro.enabled = false;
        }
    }

    void Update()
    {
        if (!isShowingText && IsPlayerBehindEnemy() && !IsPlayerInSight() && watchers.Count == 0)
        {
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

    void UpdateCanvasIcon()
    {
        if (canvas != null && iconImage != null)
        {
            iconImage.sprite = (watchers.Count > 0) ? OpenedEye : ClosedEye;
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        isShowingText = true;
        yield return new WaitForSeconds(delay);
        isShowingText = false;

        if (animator != null)
        {
            animator.StopPlayback();
        }
        if (textMeshPro != null)
        {
            textMeshPro.enabled = false;
        }
    }
}
