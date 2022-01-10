using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponData : ScriptableObject
{
    public Weapon weapon;
    public new string name;
    public float bulletSpread, fireDelay, reloadTime;
    public Vector2 fireOffset;
    public int maxAmmo, bulletsPerShot = 1;
    public AmmoType ammoType;

    public GameObject projectile;

    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip reloadSound;

    public enum Weapon { Handgun, Shotgun, Rifle }
    public enum AmmoType { Bullet, Shell }
}
