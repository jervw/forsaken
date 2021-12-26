using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public float bulletSpread, fireDelay, reloadTime;
    public int maxAmmo;
    public GameObject projectile;
    public Vector3 fireOffset;
}
