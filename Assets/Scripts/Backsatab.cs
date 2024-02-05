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
    private int textAnimationCount = 0;

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
        if (!isShowingText && IsPlayerBehindEnemy() && !IsPlayerVisibleToAnyEnemy(this, seen) && watchers.Count == 0)
        {
            textMeshPro.enabled = true;
            animator.Play("text_anim");
            StartCoroutine(HideTextAfterDelay(2f));
            textAnimationCount++;

            if (textAnimationCount >= 3)
            {
                StartCoroutine(DisableTextAfterThreeAnimations());
            }
        }

        UpdateCanvasIcon();
    }

    private bool IsPlayerBehindEnemy()
    {
        Vector3 playerToEnemy = enemyTransform.position - playerTransform.position;
        float angle = Vector3.Angle(playerTransform.forward, playerToEnemy);

        return Mathf.Abs(angle) < 90f;
    }

    private bool IsPlayerVisibleToAnyEnemy(Backsatab currentBacksatab, bool visible)
    {
        foreach (var enemy in currentBacksatab.watchers)
        {
            if (enemy.seen)
            {
                return true;
            }
        }
        return false;
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

    private IEnumerator DisableTextAfterThreeAnimations()
    {
        yield return new WaitForSeconds(2f);
        textMeshPro.enabled = false;
        textAnimationCount = 0;
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
