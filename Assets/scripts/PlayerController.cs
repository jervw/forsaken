using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

using Photon.Pun;
using Photon.Realtime;

namespace Com.Jervw.Crimson
{
    public class PlayerController : MonoBehaviourPun
    {
        public Transform firePosition, upperBody, lowerBody;
        public GameObject projectile;

        [SerializeField] float movementSpeed, weaponReloadSpeed;
        [SerializeField] float bulletSpeed, bulletSpread, fireDelay;
        [SerializeField] int weaponMaxAmmo;

        Camera cam;
        Rigidbody2D rb2d;
        Animator animator;
        Vector3 spread;
        Vector2 movementInput;
        int health, weaponCurrentAmmo;
        bool reloading, mouseShooting, canShoot;

        void Awake()
        {
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            cam = Camera.main;
            weaponCurrentAmmo = weaponMaxAmmo;
            canShoot = true;

        }

        void FixedUpdate()
        {
            if (!photonView.IsMine) return;

            movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));



            // Apply rotation to gameobject according ange of mouse position.
            var mouseScreenPosition = Input.mousePosition;
            var mouseWorldPostition = cam.ScreenToWorldPoint(mouseScreenPosition);

            var targetDirection = mouseWorldPostition - upperBody.transform.position;
            var upperBodyAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            upperBody.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, upperBodyAngle));


            if (movementInput != Vector2.zero)
            {
                var lowerBodyangle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, lowerBodyangle));
                lowerBody.transform.rotation = rotation;
            }


            // movement
            float movement = movementInput.magnitude;
            transform.Translate(movementInput.normalized * movementSpeed * Time.deltaTime);
            animator.SetFloat("isMoving", movementInput.magnitude);


            // shooting
            if (Input.GetMouseButton(0) && canShoot && AmmoCheck())
            {
                canShoot = false;
                spread = new Vector3(0, 0, Random.Range(0, 10));
                var p = PhotonNetwork.Instantiate("Bullet", firePosition.position, Quaternion.Euler(upperBody.transform.rotation.eulerAngles - spread));
                //p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * bulletSpeed, ForceMode2D.Impulse);
                weaponCurrentAmmo--;
                animator.SetTrigger("shoot");
                StartCoroutine(ShootDelay());
            }
            else if (!AmmoCheck() && canShoot)
            {
                canShoot = false;
                animator.SetTrigger("reload");
                StartCoroutine(WeaponReload());
            }


        }


        bool AmmoCheck()
        {
            return weaponCurrentAmmo > 0;
        }

        IEnumerator WeaponReload()
        {
            yield return new WaitForSeconds(weaponReloadSpeed);
            weaponCurrentAmmo = weaponMaxAmmo;
            canShoot = true;
        }

        IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(fireDelay);
            canShoot = true;
        }

        public void TakeDamage(int damage)
        {
            photonView.RPC("TakeDamageRPC", RpcTarget.All, damage);
        }

        [PunRPC]
        void TakeDamageRPC(int damage)
        {
            Debug.Log("Player took damage" + damage);
        }

        public int GetHealth()
        {
            return health;
        }
    }
}