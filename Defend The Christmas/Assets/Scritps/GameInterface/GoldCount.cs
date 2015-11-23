using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldCount : MonoBehaviour {

	// Use this for initialization
    string Format;
    int Gold;
    Text txt;
	void Start () {
        Format = "Количество золота: \n{0}";
        Gold = PlayerManagement.Gold;
        txt = this.GetComponent<Text>();
        txt.text = string.Format(Format, Gold);
	}
	
	// Update is called once per frame
	void Update () {
        if (Gold != PlayerManagement.Gold)
        {
            txt.text = string.Format(Format, PlayerManagement.Gold);
            Gold = PlayerManagement.Gold;
        }
	}
}
