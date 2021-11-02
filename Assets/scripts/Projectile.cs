using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileLifetime = 5f;

    private void Start()
    {
        StartCoroutine(WaitAndDestroy(projectileLifetime));
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}

