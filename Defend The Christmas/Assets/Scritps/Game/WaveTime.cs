using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveTime : MonoBehaviour {

    public static float WTime = 0;
    string form = "{0}";
    Text txt;
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (txt.text != string.Format(form, (int)WTime)) 
        {
            txt.text = string.Format(form, (int)WTime);
        }
	}
}
