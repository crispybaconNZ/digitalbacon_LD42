using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoButton : MonoBehaviour {
    [SerializeField] private Button goButton;
    private AudioSource music;

	// Use this for initialization
	void Start () {
        Button btn = goButton.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
        music = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void StartGame () {
        music.Stop();
        SceneManager.LoadScene("Game");
	}
}
