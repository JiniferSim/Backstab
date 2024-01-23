using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnemyDirection : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);

    }
}
