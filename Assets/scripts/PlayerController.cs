using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    int weaponMaxAmmo;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    float weaponReloadSpeed;

    [SerializeField]
    float bulletSpread;

    [SerializeField]
    float fireDelay;

    [SerializeField]
    Transform firePosition;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform upperBody;

    [SerializeField]
    Transform lowerBody;

    private Rigidbody2D rb2d;
    private Vector2 movementInput;
    private Camera mainCam;

    private int weaponCurrentAmmo;
    private bool currentlyReloading, mouseShooting, canShoot;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        mainCam = Camera.main;
        weaponCurrentAmmo = weaponMaxAmmo;
        canShoot = true;
    }

    private void Update()
    {

        if (Input.GetMouseButton(0) && canShoot && AmmoCheck())
        {
            canShoot = false;
            var spread = new Vector3(0, 0, Random.Range(0, 10));
            GameObject p = Instantiate(
                projectile, firePosition.position, Quaternion.Euler(upperBody.transform.rotation.eulerAngles - spread));
            p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * bulletSpeed, ForceMode2D.Impulse);
            weaponCurrentAmmo--;
            StartCoroutine(ShootDelay());
        }
        else if (!AmmoCheck() && canShoot)
        {
            canShoot = false;
            StartCoroutine(WeaponReload());

        }
    }

    private void FixedUpdate()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Get mouse pos and translate to world point.
        var mouseScreenPosition = Input.mousePosition;
        var mouseWorldPostition = mainCam.ScreenToWorldPoint(mouseScreenPosition);



        // Apply rotation to gameobject according ange of mouse position.
        var targetDirection = mouseWorldPostition - upperBody.transform.position;
        var upperBodyAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        upperBody.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, upperBodyAngle));


        if (movementInput != Vector2.zero)
        {
            var lowerBodyangle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, lowerBodyangle));
            lowerBody.transform.rotation = rotation;
        }


        // Movement using velocity
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0);
        rb2d.velocity = new Vector3(movementInput.x * movementSpeed, movementInput.y * movementSpeed, 0f);
    }

    private bool AmmoCheck()
    {
        return weaponCurrentAmmo > 0;
    }

    IEnumerator WeaponReload()
    {
        Debug.Log("RELOADING");
        yield return new WaitForSeconds(weaponReloadSpeed);
        Debug.Log("DONE");
        weaponCurrentAmmo = weaponMaxAmmo;
        canShoot = true;
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    private static bool AsBool(float value)
    {
        return Mathf.Approximately(Mathf.Min(value, 1), 1);
    }
}
