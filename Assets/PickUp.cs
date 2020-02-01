using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPart {
    public bool collected = false;
    //...
}


public abstract class Item {
    protected bool complete = false;
    public List<ItemPart> parts;
    protected void addPart() {
        if (!complete) {
            foreach (ItemPart part in parts) {
                if (!part.collected) {
                    part.collected = true;
                    if (Object.ReferenceEquals(part, parts[parts.Count])) {
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
    protected abstract void effect();
}


public abstract class PickUp : MonoBehaviour {
    //public Item ...; (?)

    protected abstract void onTriggerEnter2D(Collider2D other);  //_____________ GameObject.Item (?) .addPart() 

    protected void destroy(Collider2D other) {
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

    protected override void effect() {
        
    }
}
