using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float projectileLifetime = 5f;

    [SerializeField]
    float projectileSpeed = 70f;

    void Start()
    {
        //StartCoroutine(WaitAndDestroy(projectileLifetime));
    }

    void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }

    IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}

