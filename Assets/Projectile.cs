using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitAndDestroy(5f));
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }

}
