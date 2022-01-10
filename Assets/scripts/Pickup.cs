using System;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;


public class Pickup : MonoBehaviourPunCallbacks
{
    public PickupData[] pickups;

    SpriteRenderer spriteRenderer;

    GameObject effect;
    string pickupName;
    bool consumeOnPickup, isWeapon;
    float effectDuration;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int r = Random.Range(0, pickups.Length);
        SetType(pickups[r]);
    }

    void SetType(PickupData pickup)
    {
        gameObject.name = pickup.name;
        spriteRenderer.sprite = pickup.icon;
        effect = pickup.effect;
        pickupName = pickup.name;
        consumeOnPickup = pickup.consumeOnPickup;
        isWeapon = pickup.isWeapon;
        effectDuration = pickup.duration;
    }

    void Activate(GameObject player)
    {
        Debug.Log("Activating effect: " + pickupName);

        if (isWeapon)
        {
            var wpn = player.GetComponent<WeaponHandler>();

        }

        else if (consumeOnPickup)
        {
            var obj = Instantiate(effect, transform.position, Quaternion.identity, player.transform);
            obj.name = effect.name;
        }
        else
            PhotonNetwork.Instantiate(effect.name, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Activate(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}