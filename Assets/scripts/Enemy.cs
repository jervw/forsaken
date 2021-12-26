using System.Collections;
using UnityEngine;
using Com.Jervw.Crimson;

using Photon.Pun;
using Photon.Realtime;

public class Enemy : MonoBehaviourPunCallbacks
{
    [SerializeField]
    float chaseSpeed;

    [SerializeField]
    public int maxHp;

    Animator animator;

    Transform target;
    int currentHp;
    bool attacking;

    float nextAttackTime = 0f;
    float attackRate = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        target = ClosestTarget();
    }

    void FixedUpdate()
    {


        if (target != null)
        {
            float distance = (target.position - transform.position).magnitude;

            // Look at player
            transform.right = target.position - transform.position;

            // Chase player
            if (!attacking && distance > 1f)
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
                    target.gameObject.GetComponent<PlayerController>().TakeDamage(1);
                }
            }
        }
        else
            target = ClosestTarget();



        if (currentHp <= 0)
            OnDeath();

    }

    public void OnDeath()
    {
        if (Random.value <= LevelData.pickupChance)
            PhotonNetwork.Instantiate("Pickup", transform.position, Quaternion.identity);

        //animator.SetBool("isDead", true);
        Destroy(gameObject);
        LevelData.enemyDeathCount++;
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
        float tmp = chaseSpeed;
        chaseSpeed = 0;
        //animator.SetBool("isFrozen", true);
        yield return new WaitForSeconds(time);
        //animator.SetBool("isFrozen", false);
        chaseSpeed = tmp;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            currentHp -= 5;
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
