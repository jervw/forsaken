using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitAndDestroy(5f));


    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}

