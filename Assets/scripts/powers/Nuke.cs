using UnityEngine;

public class Nuke : MonoBehaviour
{
    Animation anim;

    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    void Start()
    {
        anim.Play();
        Destroy(gameObject, anim.clip.length);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.gameObject.GetComponent<Enemy>().OnDeath();
    }
}
