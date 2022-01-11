using System.Collections;
using UnityEngine;

public class InstakillEffect : MonoBehaviour
{
    public PickupData effect;

    void Awake() => StartCoroutine(EffectRoutine());

    IEnumerator EffectRoutine()
    {
        WeaponHandler wpn = transform.parent.gameObject.GetComponent<WeaponHandler>();
        if (!wpn) yield break;

        int tmp = wpn.Damage;
        wpn.Damage = 1000;

        Debug.Log("Instakill effect");
        yield return new WaitForSeconds(effect.duration);
        Debug.Log("Instakill effect ended");
        wpn.Damage = tmp;
    }
}
