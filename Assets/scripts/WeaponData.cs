using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public float bulletSpread, fireDelay, reloadTime, fireOffset;
    public int maxAmmo, bulletsPerShot = 1;
    public AmmoType ammoType;
    public enum AmmoType { Bullet, Shell }
    public GameObject projectile;
}
