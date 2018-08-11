using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private readonly float maxSpeed = 0.1f;
    [SerializeField] private readonly float rotSpeed = 10.0f;
    private Crate crate = null;    // potential crate to be carried
    [SerializeField] private SpawnManager spawnManager;
    private bool isCarrying;

    private readonly int[,] rotations = new int[3, 3] { 
        { 135, 180, 225 }, 
        { 90, 0, 270 }, 
        { 45, 0, 315 }
    };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateRotation();
        UpdatePosition();
        CheckForPickup();
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "crate") {
            crate = other.GetComponent<Crate>();
            crate.StopMoving();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "crate") {
            crate = null;
        }
    }
  

    void UpdateRotation() {
        if (Input.anyKey) {
            // Only need to update rotation if a key is actually being pressed
            int x_delta = 0, y_delta = 0;
            bool movementKeyPressed = false;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                y_delta = +1;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                y_delta = -1;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                x_delta = -1;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                x_delta = 1;
                movementKeyPressed = true;
            }

            if (!movementKeyPressed) {
                return;
            }

            float targetRotation = rotations[y_delta + 1, x_delta + 1];
            this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, Quaternion.Euler(0f, 0f, targetRotation), rotSpeed);
        }
    }

    void UpdatePosition() {
        if (Input.anyKey) {
            // only need to update position if a key is actually being pressed
            float x_delta = 0, y_delta = 0;
            bool movementKeyPressed = false;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                y_delta = +1;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                y_delta = -1;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                x_delta = -1;
                movementKeyPressed = true;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                x_delta = 1;
                movementKeyPressed = true;
            }

            if (!movementKeyPressed) {
                return;
            }

            x_delta *= maxSpeed;
            y_delta *= maxSpeed;
            Vector3 newPosition = this.gameObject.transform.position + new Vector3(x_delta, y_delta);
            newPosition.x = Mathf.Clamp(newPosition.x, -9.5f, 9.5f);
            newPosition.y = Mathf.Clamp(newPosition.y, -4.95f, 4.95f);

            // stop player straying onto conveyor belt
            SpriteRenderer rend = this.gameObject.GetComponent<SpriteRenderer>();

            if (newPosition.x > (Conveyor_Belt.end - rend.bounds.extents.x) && newPosition.y > 1.66) {
                newPosition = gameObject.transform.position ;  
            }

            // stop player straying into chute
            if (newPosition.x > 6.06 && newPosition.y < -1.72) {
                newPosition = gameObject.transform.position;
            }
            this.gameObject.transform.position = newPosition;
        }
    }

    void CheckForPickup() {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) {
           if (crate != null) {
                if (isCarrying) {
                    // we're already carrying the crate, so drop it
                    crate.transform.parent = null;
                    isCarrying = false;
                } else {
                    // otherwise pick up the crate
                    crate.transform.parent = this.transform;
                    isCarrying = true;
                }
            }
        }
    }
}
