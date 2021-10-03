using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 3f;

    [SerializeField]
    public int weaponMaxAmmo = 30;

    [SerializeField]
    public float bulletSpeed = 5f;

    [SerializeField]
    public float weaponReloadSpeed = 2f;

    [SerializeField]
    public float bulletSpread;

    [SerializeField]
    public float fireDelay = 0.2f;

    [SerializeField]
    public Transform firePosition;

    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    public Transform upperBody;

    [SerializeField]
    public Transform lowerBody;

    private Rigidbody2D rb2d;
    private InputMapper inputManager;
    private Vector2 movementInput;
    private Camera mainCam;

    private int weaponCurrentAmmo;
    private bool currentlyReloading;
    private bool mouseShooting;
    private bool canShoot = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        inputManager = new InputMapper();
        inputManager.PlayerControls.Shoot.performed += ctx => mouseShooting = AsBool(ctx.ReadValue<float>());
        inputManager.PlayerControls.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();

    }

    private void Start()
    {
        mainCam = Camera.main;
        weaponCurrentAmmo = weaponMaxAmmo;

    }

    private void Update()
    {
        Debug.Log(upperBody.transform.rotation.eulerAngles);

        if (mouseShooting && canShoot && AmmoCheck())
        {
            canShoot = false;
            var spread = new Vector3(0, 0, Random.Range(0, 90));

            GameObject p = Instantiate(projectile, firePosition.position, Quaternion.Euler(upperBody.transform.rotation.eulerAngles - spread));
            p.GetComponent<Rigidbody2D>().AddForce(p.transform.up * bulletSpeed, ForceMode2D.Impulse);
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
        // Get mouse pos and translate to world point.
        Vector2 mouseScreenPosition = inputManager.PlayerControls.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPostition = mainCam.ScreenToWorldPoint(mouseScreenPosition);


        // Apply rotation to gameobject according ange of mouse position.
        Vector3 targetDirection = mouseWorldPostition - upperBody.transform.position;
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

    private void OnEnable()
    {
        inputManager.Enable();
    }

    private void OnDisable()
    {
        inputManager.Disable();
    }
}
