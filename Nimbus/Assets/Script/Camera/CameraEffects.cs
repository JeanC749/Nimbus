using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public IEnumerator Snake(float duration, float intensity)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = originalPosition + new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), 0);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        transform.position = originalPosition;
    }

}
