﻿using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {
    public int HealthPoints;
    int MaxHealthPoints;
	// Use this for initialization
	void Start () {
        MaxHealthPoints = HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthPoints <= 0)
            GameObject.Find("ControlPanel").GetComponent<PlayerManagement>().Lose();
	}

    public float GetHealthPercent()
    {
        return (float)HealthPoints / MaxHealthPoints;
    }

    public virtual void Skip()
    {
        GameObject.Find("Field").GetComponent<Field>().Skip();
    }
}
