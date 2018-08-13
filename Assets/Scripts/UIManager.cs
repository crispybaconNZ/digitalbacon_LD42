using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private Text scoreText, scoreTextShadow;
    private Text gameOverMessage, gameOverMessageShadow;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("Score_Text").GetComponent<Text>();
        scoreTextShadow = GameObject.Find("Score_Shadow").GetComponent<Text>();
        gameOverMessage = GameObject.Find("Game_Over_Message").GetComponent<Text>();
        gameOverMessageShadow = GameObject.Find("Game_Over_Message_Shadow").GetComponent<Text>();
        ToggleGameOverMessage();
    }

	public void SetScore(int score) {
        string text = "Score: " + score.ToString("#,##0");
        scoreText.text = text;
        scoreTextShadow.text = text;
    }

    public void ToggleGameOverMessage() {
        gameOverMessage.enabled = !gameOverMessage.enabled;
        gameOverMessageShadow.enabled = !gameOverMessageShadow.enabled;
    }
}
    