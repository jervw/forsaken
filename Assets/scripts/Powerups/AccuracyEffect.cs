using System.Collections;
using UnityEngine;

public class AccuracyEffect : MonoBehaviour
{
    public PickupData effect;

    void Awake() => StartCoroutine(EffectRoutine());

    IEnumerator EffectRoutine()
    {
        var wpn = transform.parent.gameObject.GetComponent<WeaponHandler>();
        if (wpn.Spread <= 0 || !wpn) yield break;

        float tmp = wpn.Spread;

        wpn.Spread = 0;
        yield return new WaitForSeconds(effect.duration);
        wpn.Spread = tmp;
    }
}
