using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Switcher : MonoBehaviour {

    public enum WhatToSwitch { Music, Sound, Language };
    public WhatToSwitch Switching;
    public Texture2D TextureOn;
    public Texture2D TextureOff;
    Image img;
    bool isOn = true;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        CheckTexture();
	}
	
	// Update is called once per frame
	void Update () {
        CheckTexture();
	}

    void CheckTexture()
    {
        bool curOn = isOn;
        switch (Switching)
        {
            case WhatToSwitch.Music:
                curOn = Settings.MusicOn;
                break;
            case WhatToSwitch.Sound:
                curOn = Settings.SoundOn;
                break;
            case WhatToSwitch.Language:
                curOn = Settings.Russian;
                break;
        }
        if (curOn != isOn)
        {
            isOn = curOn;
            if (isOn)
                img.sprite = Sprite.Create(TextureOn, new Rect(0, 0, TextureOn.width, TextureOn.height), new Vector2(0, 0));
            else
                img.sprite = Sprite.Create(TextureOff, new Rect(0, 0, TextureOff.width, TextureOff.height), new Vector2(0, 0));
        }
    }

    public virtual void Switch()
    {
        switch (Switching)
        {
            case WhatToSwitch.Music:
                Settings.SwitchMusic();
                break;
            case WhatToSwitch.Sound:
                Settings.SwitchSound();
                break;
            case WhatToSwitch.Language:
                Settings.SwitchLanguage();
                break;
        }
    }
}
