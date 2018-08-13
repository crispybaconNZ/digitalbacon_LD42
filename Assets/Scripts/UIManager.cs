using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private Text scoreText, scoreTextShadow;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("Score_Text").GetComponent<Text>();
        scoreTextShadow = GameObject.Find("Score_Shadow").GetComponent<Text>();
	}

	public void SetScore(int score) {
        string text = "Score: " + score.ToString("#,##0");
        scoreText.text = text;
        scoreTextShadow.text = text;
    }
}
    