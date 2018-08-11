using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {
    enum CrateState { OnConveyorBelt, Scattering, Resting, Carried };
    private CrateState state = CrateState.OnConveyorBelt;

    [SerializeField] private float speed;
    private const float MIN_SPEED = 0.05f;
    private Vector3 scatterVector, scatterTarget;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "crate") {
            speed = 0.0f;
            scatterTarget = gameObject.transform.position;
            state = CrateState.Resting;
            if (gameObject.transform.position.x >= Conveyor_Belt.end) {
                Debug.Log("GAME OVER!");
            }
        }
    }

    // Update is called once per frame
    void Update () {
	    if (state == CrateState.OnConveyorBelt) {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            if (gameObject.transform.position.x <= Conveyor_Belt.end) {                
                ScatterOntoDepositArea();
            }
        } else if (state == CrateState.Scattering) {
            bool passedTarget = gameObject.transform.position.x < scatterTarget.x;
            if (!passedTarget) {
                // move towards the scatterTarget location
                gameObject.transform.Translate(scatterVector * speed * Time.deltaTime, Space.World);
            } else {
                state = CrateState.Resting;
            }
        } else {
        }
	}

    private void ScatterOntoDepositArea() {
        state = CrateState.Scattering;
        float rotation = gameObject.transform.rotation.eulerAngles.z;   // rotation in degrees
        scatterVector = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -rotation), Mathf.Cos(Mathf.Deg2Rad * -rotation)).normalized;
        float scatterDistance = Random.Range(2.0f, 5.0f);
        scatterTarget = scatterVector * scatterDistance + gameObject.transform.position;
    }
}
