using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldCount : MonoBehaviour {

	// Use this for initialization
    string RusFormat;
    string EngFormat;
    int Gold;
    Text txt;
	void Start () {
        RusFormat = "Количество золота: \n{0}";
        EngFormat = "Gold count: \n{0}";
        Gold = PlayerManagement.Gold;
        txt = this.GetComponent<Text>();
        if (Settings.Russian)
            txt.text = string.Format(RusFormat, Gold);
        else
            txt.text = string.Format(EngFormat, Gold);
	}
	
	// Update is called once per frame
	void Update () {
        if (Gold != PlayerManagement.Gold)
        {
            if (Settings.Russian)
                txt.text = string.Format(RusFormat, Gold);
            else
                txt.text = string.Format(EngFormat, Gold);
            Gold = PlayerManagement.Gold;
        }
	}
}
