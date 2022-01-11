using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ProgressBar : MonoBehaviourPunCallbacks
{
    [SerializeField] float smoothing = 2f;
    Image progressBar;

    void Awake() => progressBar = GetComponentInChildren<Image>();

    void LateUpdate() => progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, LevelHandler.Instance.GetProgress(), smoothing * Time.deltaTime);
}
