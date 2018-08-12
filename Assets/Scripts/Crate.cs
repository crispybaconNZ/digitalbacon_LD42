using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    enum CrateState { OnConveyorBelt, Scattering, Resting, Carried };
    private CrateState state = CrateState.OnConveyorBelt;
    [SerializeField] private float speed;
    private readonly float DECELERATION = 0.009f;
    [SerializeField] public int points = 1;

    public SpawnManager spawnManager;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "crate") {
            speed = 0.0f;            
            state = CrateState.Resting;
            if (gameObject.transform.position.x >= -3.449123 && gameObject.transform.position.y > 2.259) {
                Debug.Log("GAME OVER!");
                spawnManager.StopCrateSpawning();
            }
        }
    }

    public void StopMoving() {
        speed = 0.0f;
        state = CrateState.Resting;
    }

    // Update is called once per frame
    void Update () {
	    if (state == CrateState.OnConveyorBelt) {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            if (gameObject.transform.position.x <= Conveyor_Belt.end) {                
                ScatterOntoDepositArea();
            }
        } else if (state == CrateState.Scattering) {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            speed -= DECELERATION;
            if (speed <= 0.0f) {
                StopMoving();
            }
        } else {
        }

         
	}

    private void ScatterOntoDepositArea() {
        state = CrateState.Scattering;
        
    }
}
