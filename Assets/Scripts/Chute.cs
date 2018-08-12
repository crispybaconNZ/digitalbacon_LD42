using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chute : MonoBehaviour {
    [SerializeField] private SpawnManager spawnManager;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision with chute!");
        if (other.gameObject.tag == "crate") {
            Crate crate = other.GetComponent<Crate>();
            spawnManager.CrateDelivered(crate);
        }
        
    }
}
