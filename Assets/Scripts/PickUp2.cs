using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp2 : MonoBehaviour {

    /*public enum PickUpType : uint
    {
        Eye = 0,
        Head = 1,
        Leg = 2,
        OS = 3
    }
*/
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag.Equals("Player")) {
            //OnPickUpEvent.Invoke(itemID);
            var inventory = coll.gameObject.GetComponent<Inventory2>();
            if (this.gameObject.name == "Eye") {
                inventory.collect(EnumPickUpType.PickUpType.Eye);
            }
            else if (this.gameObject.name == "Head") {
                inventory.collect(EnumPickUpType.PickUpType.Head);
            }
            else if (this.gameObject.name == "Leg") {
                inventory.collect(EnumPickUpType.PickUpType.Leg);
            }
            else if (this.gameObject.name == "OS") {
                inventory.collect(EnumPickUpType.PickUpType.OS);
            }
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
        //GameObject e = Instantiate(<azione desiderata>) as GameObject;
        //e.transform.position = transform.position;
        //Destroy (other.gameObject);
        //this.gameObject.SetActive(false);
    }

}
