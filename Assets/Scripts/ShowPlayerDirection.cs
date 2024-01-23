using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerDirection : MonoBehaviour
{
    void Update()
    {
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);

    }
}
