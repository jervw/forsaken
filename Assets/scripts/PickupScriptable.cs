using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "Pickup")]
public class PickupScriptable : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public GameObject effect;
    public bool consumeOnPickup;
    public float effectDuration;
}
