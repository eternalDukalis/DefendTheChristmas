using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaCount : MonoBehaviour {

    string RusFormat;
    string EngFormat;
    int Mana;
    Text txt;
	// Use this for initialization
	void Start () {
        RusFormat = "Количество маны: \n{0} / {1}";
        EngFormat = "Mana count: \n{0} / {1}";
        Mana = PlayerManagement.Mana;
        txt = this.GetComponent<Text>();
        if (Settings.Russian)
            txt.text = string.Format(RusFormat, Mana, PlayerManagement.MaxMana);
        else
            txt.text = string.Format(EngFormat, Mana, PlayerManagement.MaxMana);
	}
	
	// Update is called once per frame
	void Update () {
        if (Mana != PlayerManagement.Mana)
        {
            if (Settings.Russian)
                txt.text = string.Format(RusFormat, Mana, PlayerManagement.MaxMana);
            else
                txt.text = string.Format(EngFormat, Mana, PlayerManagement.MaxMana);
            Mana = PlayerManagement.Mana;
        }
	}
}
