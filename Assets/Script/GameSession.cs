using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] int score = 0;
	// Use this for initialization

	void Awake() {
        SetUPSingleton();
	}

    private void SetUPSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public int getScore()
    {
        return score;
    }

    public void addToScore( int additionalScore)
    {
        score += additionalScore;
    }

    public void Restart()
    {
        Destroy(gameObject);
    }

}
