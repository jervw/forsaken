using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        switch (GameManager.Instance.State)
        {
            case GameManager.GameState.Playing:
                sprite.enabled = true;
                Cursor.visible = false;
                break;
            case GameManager.GameState.Paused:
                sprite.enabled = false;
                Cursor.visible = true;
                break;
            case GameManager.GameState.Win:
                sprite.enabled = false;
                Cursor.visible = true;
                break;
            case GameManager.GameState.Lose:
                sprite.enabled = false;
                Cursor.visible = true;
                break;
            default:
                break;
        }
    }
}
