using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemPart {
    bool collected = false;
    //...
}


public abstract class Item {
    private bool complete = false;
    public List<ItemPart> parts;
    private void addPart() {
        if (!complete) {
            foreach (ItemParts part in parts) {
                if (!part.collected) {
                    part.collected = true;
                    if (part is parts[-1]) {
                        complete = true;
                    }
                    break;
                }
            }
        }
    } 

    public void use() {
        if (complete) {
            effect();
        }
    }
    private abstract void effect();
}


public abstract class PickUp : MonoBehaviour {
    //public Item ...; (?)

    private abstract void onTriggerEnter2D(Collider2D other);  //_____________ GameObject.Item (?) .addPart() 

    private void destroy(Collider2D other) {
        Destroy (other.gameObject);
        this.gameObject.SetActive(false);
    }

}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class <nome oggetto collisione> : MonoBehaviour{
  public GameObject <azione desiderata>;

  private void OnTriggerEnter2D(Collider2D other){
    Debug.Log("hit detected");
    GameObject e = Instantiate(<azione desiderata>) as GameObject;
    e.transform.position = transform.position;
    //Destroy (other.gameObject);
    //this.gameObject.SetActive(false);
  }
}*/


// ESEMPIO
public class Rampino : Item {
    public Rampino() {
        parts = new List<ItemPart>();
        for (int i = 0; i < 3; i++) {
            parts.Add(new ItemPart());
        }
    }

    @override
    effect() {
        
    }
}
