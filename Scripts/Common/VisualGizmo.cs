using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGizmo : MonoBehaviour
{
    [SerializeField]
    private float LineLength = 50f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.right * LineLength);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.up * LineLength);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.forward * LineLength);
    }
}
