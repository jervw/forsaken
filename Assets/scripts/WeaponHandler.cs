using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

using Photon.Pun;

public class WeaponHandler : MonoBehaviourPun
{
    public Transform firePosition, upperBody;
    public WeaponData[] possibleWeapons;

    WeaponData currentWeapon;
    Animator animator;

    float currentAmmo;
    bool canShoot;

    void Awake()
    {
        animator = GetComponent<Animator>();
        SetWeapon(WeaponData.Weapon.Handgun);
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetWeapon(WeaponData.Weapon.Handgun);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SetWeapon(WeaponData.Weapon.Shotgun);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                SetWeapon(WeaponData.Weapon.Rifle);
        }
    }

    public void Shoot()
    {
        if (!CanShoot()) return;

        switch (currentWeapon.ammoType)
        {
            case WeaponData.AmmoType.Bullet:
                RegularShot();
                break;
            case WeaponData.AmmoType.Shell:
                ShellShot();
                break;
            default:
                return;
        }

        animator.SetTrigger("shoot");
        AudioManager.Instance.Play(currentWeapon.fireSound.name);
        StartCoroutine(FireDelay());

        if (currentAmmo <= 0)
        {
            AudioManager.Instance.Play("dry_shot");
            CallReload();
        }
    }

    void RegularShot()
    {
        var spread = new Vector3(0, 0, Random.Range(-currentWeapon.bulletSpread, currentWeapon.bulletSpread));
        var rotation = Quaternion.Euler(upperBody.transform.rotation.eulerAngles - spread);
        PhotonNetwork.Instantiate(currentWeapon.projectile.name, firePosition.transform.position, rotation);
        currentAmmo--;
    }

    void ShellShot()
    {
        for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
        {
            var spread = new Vector3(0, 0, Random.Range(-currentWeapon.bulletSpread, currentWeapon.bulletSpread));
            var rotation = Quaternion.Euler(upperBody.transform.rotation.eulerAngles - spread);
            PhotonNetwork.Instantiate(currentWeapon.projectile.name, firePosition.transform.position, rotation);
        }
        currentAmmo--;
    }

    public void CallReload()
    {
        StopAllCoroutines();
        if (currentAmmo < currentWeapon.maxAmmo)
            StartCoroutine(Reload());
    }

    IEnumerator Reload(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        canShoot = false;
        animator.SetTrigger("reload");
        AudioManager.Instance.Play(currentWeapon.reloadSound.name);

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

    public void SetWeapon(WeaponData.Weapon weapon)
    {
        switch (weapon)
        {
            case WeaponData.Weapon.Handgun:
                currentWeapon = possibleWeapons[0];
                animator.SetInteger("currentWeapon", 0);
                break;
            case WeaponData.Weapon.Shotgun:
                currentWeapon = possibleWeapons[2];
                animator.SetInteger("currentWeapon", 2);
                break;
            case WeaponData.Weapon.Rifle:
                currentWeapon = possibleWeapons[1];
                animator.SetInteger("currentWeapon", 1);
                break;
            default:
                currentWeapon = null;
                return;
        }

        if (currentWeapon)
        {
            canShoot = false;
            firePosition.localPosition = currentWeapon.fireOffset;
            AudioManager.Instance.Play("gun_pickup");
            StartCoroutine(Reload(0.2f));
        }
    }

    public float Spread
    {
        get { return currentWeapon.bulletSpread; }
        set { currentWeapon.bulletSpread = value; }
    }

    bool CanShoot() => currentAmmo > 0 && currentWeapon && canShoot;
}
