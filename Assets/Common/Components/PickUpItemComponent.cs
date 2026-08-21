using System;
using UnityEngine;

public class PickUpItemComponent : MonoBehaviour
{
    public event Action<Pickupable> PickedUpItem;



    private void OnTriggerEnter2D(Collider2D pickup)
    {
        Pickupable pickupable = pickup.GetComponent<Pickupable>();

        if (pickupable != null)
        {
            PickedUpItem?.Invoke(pickupable);
            pickupable.ProcessPickup(this);
        }
    }
}
