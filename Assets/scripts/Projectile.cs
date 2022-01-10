using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    [SerializeField] private float projectileSpeedMin, projectileSpeedMax, lifetime = 5f;
    [SerializeField] private int projectileDamage;

    void Awake() => Destroy(gameObject, lifetime);

    void FixedUpdate() => transform.Translate(Vector3.right * Random.Range(projectileSpeedMin, projectileSpeedMax) * Time.fixedDeltaTime);

    public int GetDamage() => projectileDamage;
}
