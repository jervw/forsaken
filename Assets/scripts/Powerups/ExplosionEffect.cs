using UnityEngine;
using Com.Jervw.Crimson;

public class ExplosionEffect : MonoBehaviour
{
    public Sound sound;

    [SerializeField] float maxRadius, rate = 10f;

    CircleCollider2D circleCollider;
    float size = 0f;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start() => AudioManager.Instance.Play(sound.name);

    void FixedUpdate()
    {
        if (size < maxRadius)
        {
            size += rate * Time.fixedDeltaTime;
            this.transform.localScale = new Vector3(size, size, 1f);
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
