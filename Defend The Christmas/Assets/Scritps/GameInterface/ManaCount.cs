using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaCount : MonoBehaviour {

    string Format;
    int Mana;
    Text txt;
	// Use this for initialization
	void Start () {
        Format = "Количество маны: \n{0} / {1}";
        Mana = PlayerManagement.Mana;
        txt = this.GetComponent<Text>();
        txt.text = string.Format(Format, Mana, PlayerManagement.MaxMana);
	}
	
	// Update is called once per frame
	void Update () {
        if (Mana != PlayerManagement.Mana)
        {
            txt.text = string.Format(Format, Mana, PlayerManagement.MaxMana);
            Mana = PlayerManagement.Gold;
        }
	}
}
