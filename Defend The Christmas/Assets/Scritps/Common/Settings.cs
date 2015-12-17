using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    static public bool MusicOn = true;
    static public bool SoundOn = true;
    static public bool Russian = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void SwitchMusic()
    {
        MusicOn = !MusicOn;
    }

    static public void SwitchSound()
    {
        SoundOn = !SoundOn;
    }

    static public void SwitchLanguage()
    {
        Russian = !Russian;
    }
}
