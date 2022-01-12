using UnityEngine;


public class Blood : MonoBehaviour
{
    public Sprite[] bloodSprite;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bloodSprite[Random.Range(0, bloodSprite.Length)];
        float scale = Random.Range(0.3f, 1f);
        transform.localScale = new Vector2(scale, scale);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
}
