using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private readonly float maxSpeed = 0.1f;
    [SerializeField] private readonly float rotSpeed = 10.0f;
    private Crate crate = null;    // potential crate to be carried
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private LineRenderer linePrefab;
    private const float PICKUP_DISTANCE = 1.25f;

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

    /*
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision with collider");
        if (other.tag == "crate") {
            Debug.Log("Possible to pick up " + other.name);
            crate = other.GetComponent<Crate>();
            crate.StopMoving();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "crate") {
            Debug.Log("No longer possible to pick up " + other.name);
            crate = null;
        }
    }
    */

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
            if (newPosition.x > 5.69 && newPosition.y < -1.19) {
                newPosition = gameObject.transform.position;
            }
            this.gameObject.transform.position = newPosition;
        }
    }

    void CheckForPickup() {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) {
            // see if there's a crate in front of the player
            SpriteRenderer rend = this.gameObject.GetComponent<SpriteRenderer>();
            float rotation = gameObject.transform.rotation.z;
            Vector2 playerFacing = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -rotation), Mathf.Cos(Mathf.Deg2Rad * -rotation));

            foreach (Crate candidate in spawnManager.crates) {
               /* Vector3 directionToTarget = transform.position - candidate.transform.position;
                float angle = Vector3.Angle(playerFacing, directionToTarget) - 180; 
                float distance = directionToTarget.magnitude;
                Debug.Log("Angle: " + angle + ", distance: " + distance);*/
                CreateLineRenderer(candidate);
                
                // if (Mathf.Abs(angle) < 45 && distance <= PICKUP_DISTANCE) {
                if (Physics.Raycast(transform.position, candidate.transform.position, PICKUP_DISTANCE)) {
                    Debug.Log("Can pick up " + candidate.tag + " " + candidate.name);
                } else {
                    Debug.Log("Nothing there!");
                }
            }
        }
    }

    private void CreateLineRenderer(Crate other) {
        LineRenderer linerend = Instantiate<LineRenderer>(linePrefab);
        linerend.useWorldSpace = true;
        linerend.positionCount = 2;
        linerend.SetPosition(0, transform.position);
        linerend.SetPosition(1, other.transform.position);

    }
}
