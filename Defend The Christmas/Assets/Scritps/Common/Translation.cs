using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Translation : MonoBehaviour {

    public string RussianString;
    public string EnglishString;
    Text txt;
    bool rus;
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        rus = Settings.Russian;
        SetText();
	}
	
	// Update is called once per frame
	void Update () {
        if (rus != Settings.Russian)
        {
            rus = Settings.Russian;
            SetText();
        }
	}

    public void SetText()
    {  
        if (Settings.Russian)
            txt.text = RussianString;
        else
            txt.text = EnglishString;
    }
}
