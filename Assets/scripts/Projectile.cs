using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    [SerializeField] private float projectileSpeed, lifetime = 5f;
    [SerializeField] private int projectileDamage;

    void Awake() => Destroy(gameObject, lifetime);

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.fixedDeltaTime);
    }

    public int GetDamage() { return projectileDamage; }
}
