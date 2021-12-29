using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;

    static float lifetime = 5f;

    void Start()
    {
        photonView.RPC("DestroyProjectile", RpcTarget.All);

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.fixedDeltaTime);
    }

    [PunRPC]
    void DestroyProjectile() { Destroy(gameObject, lifetime); }

    public int GetDamage() { return projectileDamage; }
}
