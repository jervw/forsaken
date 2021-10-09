using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

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
        transform.right = target.position - transform.position;

        if (currentHp <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
            currentHp -= 5;


    }
}
