using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {

    public int Level;
    Button btn;
	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
        if (Level > Settings.MaxLevel)
            btn.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void PlayGame(int level)
    {
        Field.CurrentLevel = level;
        Field.Reset();
        Application.LoadLevel("game");
    }
}
