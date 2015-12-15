﻿using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {

    public GameObject WinScreen;
    public GameObject LoseScreen;
    static public int Gold = 100;
    static public int MaxMana = 100;
    static public int Mana;
    static float RestorationPeriod = 1;
    static int RestorationSize = 1;
	// Use this for initialization
	void Start () {
        Mana = MaxMana;
        StartCoroutine(ManaRestoration());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator ManaRestoration()
    {
        float tm = 0;
        while (true)
        {
            tm += Time.deltaTime;
            if (tm > RestorationPeriod)
            {
                tm = 0;
                if (Mana<MaxMana)
                    Mana += RestorationSize;
            }
            yield return null;
        }
    }

    public void Win()
    {
        WinScreen.SetActive(true);
    }

    public void Lose()
    {
        LoseScreen.SetActive(true);
    }

    static public void Reset()
    {
        Gold = 100;
        Mana = MaxMana;
    }
}
