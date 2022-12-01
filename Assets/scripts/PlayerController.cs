using UnityEngine;
using Random = UnityEngine.Random;


namespace Com.Jervw.Crimson
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject projectile;
        public Transform firePosition, upperBody, lowerBody;

        [SerializeField] float movementSpeed, health;

        Camera cam;
        WeaponHandler weaponHandler;
        Rigidbody2D rb2d;
        Animator animator;
        Vector2 movementInput;


        void Awake()
        {
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            weaponHandler = GetComponent<WeaponHandler>();
            cam = Camera.main;
        }

        void Update()
        {
            if (GameManager.Instance.State != GameManager.GameState.Playing) return;

            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (movementInput != Vector2.zero)
            {
                var lowerBodyangle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, lowerBodyangle));
                lowerBody.transform.rotation = rotation;
            }

            transform.Translate(movementInput.normalized * movementSpeed * Time.deltaTime);
            animator.SetFloat("isMoving", movementInput.magnitude);

            // Apply rotation to gameobject according ange of mouse position.
            var mouseScreenPosition = Input.mousePosition;
            var mouseWorldPostition = cam.ScreenToWorldPoint(mouseScreenPosition);

            var targetDirection = mouseWorldPostition - upperBody.transform.position;
            var upperBodyAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            upperBody.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, upperBodyAngle));

            weaponHandler.Moving = Mathf.Clamp01(movementInput.magnitude);

            if (Input.GetMouseButton(0))
                weaponHandler.Shoot();
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public float GetHealth() => health;
    }
}