using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    enum CrateState { OnConveyorBelt, Scattering, Resting, Carried };
    private CrateState state = CrateState.OnConveyorBelt;
    [SerializeField] private float speed;
    private readonly float DECELERATION = 0.005f;
    [SerializeField] public int points = 1;
    private AudioSource pickedUp;

    public SpawnManager spawnManager;
    private UIManager uiManager;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "crate") {
            speed = 0.0f;            
            state = CrateState.Resting;
            if (gameObject.transform.position.x >= -3.449123 && gameObject.transform.position.y > 2.259) {
                spawnManager.StopCrateSpawning();
                uiManager.ShowGameOverMessage();
            }
        }
    }

    public void StopMoving() {
        speed = 0.0f;
        state = CrateState.Resting;
    }

    void Start() {
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
        pickedUp = GetComponent<AudioSource>();
    }

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

    public void PickedUp() {
        pickedUp.Play();
    }

    private void ScatterOntoDepositArea() {
        state = CrateState.Scattering;
        
    }
}
