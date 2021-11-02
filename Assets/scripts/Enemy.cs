using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float chaseSpeed;

    private Transform target;

    public int maxHp;
    private int currentHp;
    void Start()
    {
        currentHp = maxHp;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = (target.position - transform.position).magnitude;

        // Look at player
        transform.right = target.position - transform.position;

        // Chase player
        if (distance > 1)
            transform.Translate(new Vector2(chaseSpeed * Time.deltaTime, 0));

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
