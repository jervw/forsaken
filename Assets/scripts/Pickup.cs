using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Pickup : MonoBehaviourPunCallbacks
{
    public PickupData[] pickups;

    SpriteRenderer spriteRenderer;

    GameObject effect;
    bool consumeOnPickup;
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
        consumeOnPickup = pickup.consumeOnPickup;
        effectDuration = pickup.effectDuration;
    }

    void Activate(GameObject player)
    {
        if (effect == null) return;
        Debug.Log("Activating effect: " + effect.name);

        if (consumeOnPickup)
            Instantiate(effect, transform.position, Quaternion.identity, player.transform);


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