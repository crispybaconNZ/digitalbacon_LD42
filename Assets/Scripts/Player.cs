using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private readonly float maxSpeed = 0.1f;
    [SerializeField] private readonly float rotSpeed = 10.0f;

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
	}

    void UpdateRotation() {
        if (Input.anyKey) {
            // Only need to update rotation if a key is actually being pressed
            int x_delta = 0, y_delta = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                y_delta = 1;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                y_delta = -1;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                x_delta = -1;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                x_delta = 1;
            }

            float targetRotation = rotations[y_delta + 1, x_delta + 1];
            this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, Quaternion.Euler(0f, 0f, targetRotation), rotSpeed);
        }
    }

    void UpdatePosition() {
        if (Input.anyKey) {
            // only need to update position if a key is actually being pressed
            float x_delta = 0, y_delta = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                y_delta = +1;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                y_delta = -1;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                x_delta = -1;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                x_delta = 1;
            }

            x_delta *= maxSpeed;
            y_delta *= maxSpeed;
            Vector3 newPosition = this.gameObject.transform.position + new Vector3(x_delta, y_delta);
            newPosition.x = Mathf.Clamp(newPosition.x, -8.0f, 8.0f);
            newPosition.y = Mathf.Clamp(newPosition.y, -4.3f, 4.3f);
            this.gameObject.transform.position = newPosition;
        }
    }
}
