using System.Collections;
using UnityEngine;

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
                transform.Translate(new Vector2(chaseSpeed * Time.deltaTime, 0));
            }
            else
            {
                animator.SetBool("isAttacking", true);
                Debug.Log("Attacking");
            }
        }
        else
            target = ClosestTarget();



        if (currentHp <= 0)
        {
            //animator.SetBool("isDead", true);
            Destroy(gameObject);
            LevelData.enemyDeathCount++;
        }
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

    IEnumerator DealDamage()
    {

        yield return new WaitForSeconds(1);
    }


}
