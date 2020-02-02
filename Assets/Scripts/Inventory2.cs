using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {
    protected bool collected = false;
    public void collect() {
        collected = true;
    } 

    protected abstract void effect();
    

    public void use() {
        if (collected) {
            effect();
        }
    }

    public void repair() {
        if (collected) {
            collected = false;
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



public class Inventory2 : MonoBehaviour {

    public List<Item> items = new List<Item>();

    public Inventory2() {
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
    public void repair(EnumPickUpType.PickUpType p) {
        items[(int) p].repair();
    }
}