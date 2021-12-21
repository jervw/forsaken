using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int effectValue;
    float effectDuration;

    public void LoadData(PickupScriptable pickup)
    {
        this.name = pickup.name;
        GetComponent<SpriteRenderer>().sprite = pickup.icon;
        effectValue = pickup.effectValue;
        effectDuration = pickup.effectDuration;
    }
}
