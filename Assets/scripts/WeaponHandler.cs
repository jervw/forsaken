using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

using Photon.Pun;

public class WeaponHandler : MonoBehaviourPun
{
    public Transform firePosition, upperBody;
    public WeaponData[] possibleWeapons;
    public enum WeaponType : int { Pistol, Shotgun, Rifle }

    WeaponData currentWeapon;
    Animator animator;
    Coroutine isReloading;
    float currentAmmo;
    bool canShoot;

    void Awake()
    {
        animator = GetComponent<Animator>();
        SetWeapon(WeaponType.Shotgun);

    }



    public void Shoot()
    {
        if (!CanShoot()) return;

        var position = firePosition.position + currentWeapon.fireOffset;
        var spread = new Vector3(0, 0, Random.Range(0, currentWeapon.bulletSpread));
        var rotation = Quaternion.Euler(upperBody.transform.rotation.eulerAngles - spread);
        PhotonNetwork.Instantiate(currentWeapon.projectile.name, position, rotation);

        animator.SetTrigger("shoot");
        currentAmmo--;
        StartCoroutine(FireDelay());

        if (currentAmmo <= 0)
            CallReload();
    }

    public void CallReload()
    {
        StopAllCoroutines();
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        canShoot = false;
        animator.SetTrigger("reload");
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        currentAmmo = currentWeapon.maxAmmo;
        canShoot = true;
    }

    IEnumerator FireDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(currentWeapon.fireDelay);
        canShoot = true;
    }

    public void SetWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Pistol:
                currentWeapon = possibleWeapons[0];
                animator.SetInteger("currentWeapon", 0);
                break;
            case WeaponType.Shotgun:
                currentWeapon = possibleWeapons[1];
                animator.SetInteger("currentWeapon", 1);
                break;
            case WeaponType.Rifle:
                currentWeapon = possibleWeapons[2];
                animator.SetInteger("currentWeapon", 2);
                break;
            default:
                currentWeapon = null;
                break;
        }
        Debug.Log("Switched to " + currentWeapon.name);

        if (currentWeapon != null)
        {
            currentAmmo = currentWeapon.maxAmmo;
            canShoot = true;
        }
    }

    bool CanShoot()
    {
        return currentAmmo > 0 && currentWeapon != null && canShoot;
    }


}
