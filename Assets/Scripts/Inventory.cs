using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {
    protected bool collected = false;
    protected bool used = false;

    public void collect() {
        collected = true;
    } 

    protected abstract void effect();

    public void use() {

        if (collected && !used) {
            effect();
            used = true;
            Debug.Log("using " + this.GetType());
        } else if (used) {
            Debug.Log("already used " + this.GetType());
        } else {
            Debug.Log("didn't pick up " + this.GetType());
        }
    }

    public void repair() {
        if (collected && !used) {
            used = true;
            Debug.Log("repaired " + this.GetType());
        }
    }
}

public class Eye : Item {
    protected override void effect() {
    }
}

public class Head : Item {
    protected override void effect() {
    }
}

public class Leg : Item {
    protected override void effect() {
    }
}

public class OS : Item {
    protected override void effect() {
    }
}



public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();

    void Awake() {
        items.Add(new Eye());
        items.Add(new Head());
        items.Add(new Leg());
        items.Add(new OS());
    }


    public void use(EnumPickUpType.PickUpType p) {
        items[(int) p].use();
    }
    public void collect(EnumPickUpType.PickUpType p) {
        items[(int) p].collect();
    }
    public void repair(EnumPickUpType.PickUpType p, GameObject npc) {
        items[(int) p].repair();
        npc.GetComponent<NPCStatus>().OnRepair();
    }
}