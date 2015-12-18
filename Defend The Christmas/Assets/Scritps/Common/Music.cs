using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

    AudioSource AS;
	// Use this for initialization
	void Start () {
        AS = GetComponent<AudioSource>();
        AS.mute = !Settings.MusicOn;
	}
	
	// Update is called once per frame
	void Update () {
        if (AS.mute == Settings.MusicOn)
            AS.mute = !Settings.MusicOn;
	}
}
