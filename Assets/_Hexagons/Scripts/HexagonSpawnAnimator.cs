using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpawnAnimator : MonoBehaviour
{
    public Vector3 OffScreenSpawnPosition;
    public float Speed = 6f;
    public float NearEnough = 0.05f;
    private Vector3 destination;

    public void SetDestination(Vector3 dest, float delayInSecs)
    {
        if (Application.isPlaying)
        {
            destination = dest;
            transform.position = OffScreenSpawnPosition;
            StartCoroutine(Animate(delayInSecs));
        }
#if UNITY_EDITOR
        else
        {
            transform.position = dest;
            DestroyImmediate(this);
        } 
#endif
    }

    private IEnumerator Animate(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        while (Vector3.Distance(transform.position, destination) > NearEnough)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * Speed);
            yield return null;
        }
        transform.position = destination;
        Destroy(this);
    }
}
