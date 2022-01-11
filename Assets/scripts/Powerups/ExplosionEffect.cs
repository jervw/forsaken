using UnityEngine;
using Com.Jervw.Crimson;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] float maxRadius, rate = 10f;
    CircleCollider2D circleCollider;

    float size = 0f;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        if (size < maxRadius)
        {
            size += rate * Time.fixedDeltaTime;
            transform.localScale = new Vector2(size, size);
        }
        else
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.gameObject.GetComponent<Enemy>().OnDeath(false);
    }
}
