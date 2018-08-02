using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLookAt : MonoBehaviour
{
    public Transform parent;
    public Transform target;
    public Transform cube;
    public float distance;
    public float diffAngle;
    public float angle;
    public Vector3 dirToTarget;
    public Vector3 dirToChild;
    public Vector3 offset;

    private void Update()
    {
        dirToTarget = target.position - parent.position;
        dirToTarget = Vector3.ProjectOnPlane(dirToTarget, parent.up);
        dirToChild = parent.position - transform.position;

        diffAngle = Vector3.SignedAngle(dirToTarget, parent.forward, -parent.up);

        angle = transform.eulerAngles.y + diffAngle;

        float xPos = Mathf.Sin(diffAngle * Mathf.Deg2Rad) * distance;
        float ZPos = Mathf.Cos(diffAngle * Mathf.Deg2Rad) * distance;

        transform.localPosition = new Vector3(xPos, transform.localPosition.y, ZPos);

        cube.LookAt(target);
        cube.localEulerAngles = new Vector3(offset.x, offset.y + angle, offset.z);
    }
}
