using UnityEngine;

public class Projectile : MonoBehaviour
{
    public WeaponData weapon;
    int damage;

    void Awake()
    {
        damage = weapon.projectileDamage;
    }

    void Start() => Destroy(gameObject, weapon.projectileLifetime);

    void FixedUpdate() => transform.Translate(Vector3.right * Random.Range(weapon.projectileSpeedMin, weapon.projectileSpeedMax) * Time.fixedDeltaTime);

    public int Damage { get => damage; set => damage = value; }
}
