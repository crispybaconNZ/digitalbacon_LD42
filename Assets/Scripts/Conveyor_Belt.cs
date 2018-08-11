using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor_Belt : MonoBehaviour {
    private readonly float animationSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        AnimateConveyorBelt();
	}

    private void AnimateConveyorBelt() {
        Debug.Log("AnimateConveyorBelt");
        Vector2 offset = gameObject.GetComponent<Renderer>().material.mainTextureOffset;
        float new_offset = offset.x + (animationSpeed * Time.deltaTime);
        gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(new_offset, offset.y);
    }
}
