using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private Text scoreText, scoreTextShadow;
    private Text gameOverMessage, gameOverMessageShadow;
    private AudioSource gameOver;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("Score_Text").GetComponent<Text>();
        scoreTextShadow = GameObject.Find("Score_Shadow").GetComponent<Text>();
        gameOverMessage = GameObject.Find("Game_Over_Message").GetComponent<Text>();
        gameOverMessageShadow = GameObject.Find("Game_Over_Message_Shadow").GetComponent<Text>();
        HideGameOverMessage();
        gameOver = GetComponent<AudioSource>();
    }

	public void SetScore(int score) {
        string text = "Score: " + score.ToString("#,##0");
        scoreText.text = text;
        scoreTextShadow.text = text;
    }

    public void HideGameOverMessage() {
        gameOverMessage.enabled = false;
        gameOverMessageShadow.enabled = false;
        if (gameOver == null) {
            gameOver = GetComponent<AudioSource>();
        }
    }

    public void ShowGameOverMessage() {
        gameOverMessage.enabled = true;
        gameOverMessageShadow.enabled = true;
        if (gameOver != null) {
            gameOver.Play();
            gameOver = null;    // only play it once
        }
    }
}
    