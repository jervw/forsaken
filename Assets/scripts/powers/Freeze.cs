using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField]
    float freezeTime;

    void Start()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
            StartCoroutine(enemy.GetComponent<Enemy>().FreezeEnemy(freezeTime));
    }
}
