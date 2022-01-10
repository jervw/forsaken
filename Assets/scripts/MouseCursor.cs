using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    void Awake() => Cursor.visible = false;

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if (GameManager.Instance.State == GameManager.GameState.Paused)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }
}
