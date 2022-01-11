using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


namespace Com.Jervw.Crimson
{
    public class Enemy : MonoBehaviourPun
    {
        [SerializeField] int maxHp;
        [SerializeField] float chaseSpeed, attackRate, attackRange;

        Animator animator;
        ParticleSystem hitParticles;
        SpriteRenderer spriteRenderer;
        Transform target;

        int currentHp;
        bool attacking, frozen = false;
        float nextAttackTime;

        void Start()
        {
            animator = GetComponent<Animator>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            currentHp = maxHp;
            target = ClosestTarget();
        }

        void Update()
        {

            if (target && !frozen)
            {
                float distance = (target.position - transform.position).magnitude;

                // Look at player
                transform.right = target.position - transform.position;

                // Chase player
                if (!attacking && distance > attackRange)
                {
                    animator.SetBool("isMoving", true);
                    animator.SetBool("isAttacking", false);
                    CancelInvoke("Attack");
                    transform.Translate(new Vector2(chaseSpeed * Time.deltaTime, 0));
                }
                else
                {
                    animator.SetBool("isAttacking", true);
                    if (Time.time > nextAttackTime)
                    {
                        nextAttackTime = Time.time + attackRate;
                        Debug.Log("Attacking");
                        AudioManager.Instance.Play("zombie_bite");
                        target.gameObject.GetComponent<PlayerController>().TakeDamage(1);
                    }
                }
            }
            else
                target = ClosestTarget();



            if (currentHp <= 0)
                OnDeath();
        }

        public void OnDeath(bool drop = true)
        {
            photonView.RPC("OnDeathRPC", RpcTarget.All);

            if (drop && Random.value * 100 <= (LevelHandler.Instance.current.pickupChance))
                PhotonNetwork.Instantiate("Pickup", transform.position, Quaternion.identity);
        }


        [PunRPC]
        void OnDeathRPC()
        {
            //animator.SetBool("isDead", true);
            Destroy(gameObject);
            LevelHandler.Instance.EnemyDeath();
        }


        Transform ClosestTarget()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
            Transform bestTarget = null;
            float closestDistance = Mathf.Infinity;
            foreach (var target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestTarget = target.transform;
                }

                Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            return bestTarget;
        }

        public IEnumerator FreezeEnemy(float time)
        {
            frozen = true;
            animator.enabled = false;
            spriteRenderer.color = Color.cyan;
            yield return new WaitForSeconds(time);
            spriteRenderer.color = Color.white;
            frozen = false;
            animator.enabled = true;
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Bullet")
            {
                currentHp -= other.gameObject.GetComponent<Projectile>().Damage;
                hitParticles.Play();
                PhotonNetwork.Instantiate("Blood", transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }

            else if (other.tag == "Player")
                if (!attacking)
                    attacking = true;

        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
                attacking = false;
        }
    }
}