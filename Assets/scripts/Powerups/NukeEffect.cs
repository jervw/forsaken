using UnityEngine;
using Com.Jervw.Crimson;

public class NukeEffect : MonoBehaviour
{
    Animation anim;

    void Awake()
    {
        anim = GetComponent<Animation>();
        Destroy(gameObject, anim.clip.length);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.gameObject.GetComponent<Enemy>().OnDeath(false);
    }
}
