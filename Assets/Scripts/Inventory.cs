using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PickUps : uint
{
    Eye = 1,
    Leg = 2,
    Head = 4,
    OS = 8
}

public class Inventory : MonoBehaviour
{
    private uint items = 0;
    public void OnCollectiblePickUp(uint pickup) {
        items |= pickup;
        /*Debug.Log("Got #" + pickup + ", mask: #" + items);
        switch (pickup) {
            case (uint)PickUps.Eye: Debug.Log("Picked up an eye"); break;
            case (uint)PickUps.Leg: Debug.Log("Picked up a leg"); break;
            case (uint)PickUps.Head: Debug.Log("Picked up a head"); break;
            case (uint)PickUps.OS: Debug.Log("Picked up an OS backup"); break;
            default: Debug.Log("Picked up an unknown item"); break;
        }/**/
    }

    public bool CarryingItem(uint item) {
        return 0 < (items & item);
    }

    public void UseItem(uint item) {
        items &= ~item;
    }
}
