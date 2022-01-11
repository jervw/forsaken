using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon")]
    public Weapon weapon;
    public new string name;
    public float fireDelay, reloadTime;
    public Vector2 fireOffset;
    public int maxAmmo, bulletsPerShot = 1;
    public AmmoType ammoType;

    [Header("Projectile")]
    public GameObject projectile;
    public float bulletSpread, projectileSpeedMin, projectileSpeedMax;
    public int projectileDamage, projectileLifetime;

    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip reloadSound;

    public enum Weapon { Handgun, Shotgun, Rifle }
    public enum AmmoType { Bullet, Shell }
}
