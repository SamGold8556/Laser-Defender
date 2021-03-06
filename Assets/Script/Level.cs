﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] float delayInSeconds = 2.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void LoadGameOver()
    {

        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Quit Screen");

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
        FindObjectOfType<GameSession>().Restart();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level 1");
        //FindObjectOfType<GameSession>().Restart();

    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Title Screen");
        FindObjectOfType<GameSession>().Restart();
        

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
