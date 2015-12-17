﻿using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void PlayGame(int level)
    {
        Field.CurrentLevel = level;
        Field.Reset();
        Application.LoadLevel("game");
    }
}
