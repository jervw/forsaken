using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    [SerializeField] private float projectileSpeed, projectileDamage;

    static float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.fixedDeltaTime);
    }

    public float GetDamage() { return projectileDamage; }
}

