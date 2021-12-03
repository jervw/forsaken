using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float chaseSpeed;

    [SerializeField]
    public int maxHp;

    Transform target;
    int currentHp;

    void Start()
    {
        currentHp = maxHp;
        target = ClosestTarget();
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
        }
        return bestTarget;
    }

    void Update()
    {
        if (target != null)
        {
            float distance = (target.position - transform.position).magnitude;

            // Look at player
            transform.right = target.position - transform.position;

            // Chase player
            if (distance > 1)
                transform.Translate(new Vector2(chaseSpeed * Time.deltaTime, 0));
        }

        if (currentHp <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
            currentHp -= 5;
        else if (other.tag == "Player")
            Debug.Log(gameObject.name + " collided with player");
    }
}
