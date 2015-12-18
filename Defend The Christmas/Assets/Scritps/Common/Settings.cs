using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    static public bool MusicOn = true;
    static public bool SoundOn = true;
    static public bool Russian = true;
    static public int MaxLevel = 0;
	// Use this for initialization
	void Start () {
        Load();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void SwitchMusic()
    {
        MusicOn = !MusicOn;
        PlayerPrefs.SetInt("MusicOn", MusicOn.GetHashCode());
    }

    static public void SwitchSound()
    {
        SoundOn = !SoundOn;
        PlayerPrefs.SetInt("SoundOn", SoundOn.GetHashCode());
    }

    static public void SwitchLanguage()
    {
        Russian = !Russian;
        PlayerPrefs.SetInt("Russian", Russian.GetHashCode());
    }

    static public void UpdateMaxLevel(int lvl)
    {
        if (lvl > MaxLevel)
        {
            MaxLevel = lvl;
            PlayerPrefs.SetInt("MaxLevel", MaxLevel);
        }
    }

    static void Load()
    {
        if (PlayerPrefs.HasKey("MusicOn"))
            MusicOn = PlayerPrefs.GetInt("MusicOn").Equals(1);
        if (PlayerPrefs.HasKey("SoundOn"))
            SoundOn = PlayerPrefs.GetInt("SoundOn").Equals(1);
        if (PlayerPrefs.HasKey("Russian"))
            Russian = PlayerPrefs.GetInt("Russian").Equals(1);
        if (PlayerPrefs.HasKey("MaxLevel"))
            MaxLevel = PlayerPrefs.GetInt("MaxLevel");
    }
}
