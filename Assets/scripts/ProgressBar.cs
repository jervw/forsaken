using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    float smoothing = 2f;

    Image progressBar;

    void Awake()
    {
        progressBar = GetComponentInChildren<Image>();
    }

    void LateUpdate()
    {
        float progress = 1 - LevelData.GetProgress();
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress, smoothing * Time.deltaTime);
    }
}
