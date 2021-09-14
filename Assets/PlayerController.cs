using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed;

    [SerializeField]
    public Transform firePosition;

    [SerializeField]
    public GameObject projectile;

    private Rigidbody2D rb2d;
    private InputMap inputManager;
    private Vector2 movementInput;



    private Camera main;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        inputManager = new InputMap();

        inputManager.PlayerControls.Shoot.performed += _ => Shoot();
        inputManager.PlayerControls.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    }

    private void Start()
    {
        main = Camera.main;
    }

    private void Update()
    {
        Vector2 mouseScreenPosition = inputManager.PlayerControls.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPostition = main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPostition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }


    private void FixedUpdate()
    {
        // Mouse position
        Vector2 mousePos = inputManager.PlayerControls.MousePosition.ReadValue<Vector2>();
        mousePos = main.ScreenToWorldPoint(mousePos);

        // Rotate player with mouse position


        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0);
        rb2d.velocity = new Vector3(movementInput.x * movementSpeed, movementInput.y * movementSpeed, 0f);

        // rb2d.MovePosition(transform.position + movement * movementSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        Debug.Log("Shoot test ");

        Vector2 mousePos = inputManager.PlayerControls.MousePosition.ReadValue<Vector2>();
        mousePos = main.ScreenToWorldPoint(mousePos);

        GameObject p = Instantiate(projectile, firePosition.position, firePosition.rotation);
        p.GetComponent<Rigidbody2D>().velocity = firePosition.position * 5f;
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
