using UnityEngine;
using UnityEngine.Events;


public class NPCStatus : MonoBehaviour {
    private bool repaired = false;
    private bool dead = false;

    void FixedUpdate() {
        if (transform.position.y < -10) {
            dead = true;
            Debug.Log("#rip" + this.gameObject.name);
            this.gameObject.SetActive(false);
        }
    }

    public void OnRepair() {
        Debug.Log("Repaired " + this.gameObject.name);
        repaired = true;
    }
}
