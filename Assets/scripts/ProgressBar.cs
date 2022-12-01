using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] float smoothing = 2f;
    Image progressBar;

    void Awake() => progressBar = GetComponentInChildren<Image>();

    void LateUpdate() => progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, LevelHandler.Instance.GetProgress(), smoothing * Time.deltaTime);
}
