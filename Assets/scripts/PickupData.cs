using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "ScriptableObjects/Pickup")]
public class PickupData : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public GameObject effect;
    public bool consumeOnPickup, isWeapon;
    public float duration;
}
