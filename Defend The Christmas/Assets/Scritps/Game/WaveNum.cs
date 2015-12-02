using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveNum : MonoBehaviour {

    static public int Wave = -1;
    Text txt;
    string form = "{0}";
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (txt.text != string.Format(form, Wave + 1))
        {
            txt.text = string.Format(form, Wave + 1);
        }
	}
}
