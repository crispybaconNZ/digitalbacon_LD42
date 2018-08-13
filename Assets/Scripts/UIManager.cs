using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    private Text scoreText, scoreTextShadow;
    private Text gameOverMessage, gameOverMessageShadow;
    private AudioSource gameOver;
    private Button exitButton;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("Score_Text").GetComponent<Text>();
        scoreTextShadow = GameObject.Find("Score_Shadow").GetComponent<Text>();
        gameOverMessage = GameObject.Find("Game_Over_Message").GetComponent<Text>();
        gameOverMessageShadow = GameObject.Find("Game_Over_Message_Shadow").GetComponent<Text>();
        exitButton = GameObject.Find("Exit_Button").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitButtonClicked);

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
        exitButton.gameObject.SetActive(false);
        

        if (gameOver == null) {
            gameOver = GetComponent<AudioSource>();
        }
    }

    public void ShowGameOverMessage() {
        gameOverMessage.enabled = true;
        gameOverMessageShadow.enabled = true;
        exitButton.gameObject.SetActive(true);

        if (gameOver != null) {
            gameOver.Play();
            gameOver = null;    // only play it once
        }
    }

    private void ExitButtonClicked() {
        SceneManager.LoadScene("WelcomeScreen");
    }
}
    