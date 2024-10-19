using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float followSpeed = 2f, yOffset = 1f;
    [SerializeField] private Transform target;

    private void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = new(target.position.x, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, targetPosition,
             followSpeed * Time.deltaTime);
        }
    }
}
