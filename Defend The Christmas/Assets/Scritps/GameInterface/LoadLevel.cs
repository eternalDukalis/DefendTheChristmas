using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

    Field fld;
	// Use this for initialization
	void Start () {
        fld = GameObject.Find("Field").GetComponent<Field>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void UpdateLevel(bool iterate)
    {
        Field.Reset();
        GameObject.Find("Field").GetComponent<Field>().Stop();
        if (iterate)
        {
            Field.CurrentLevel++;
            if (Field.CurrentLevel == fld.GameLevels.Length)
            {
                Application.LoadLevel("menu");
                return;
            }
        }
        Application.LoadLevel("game");
    }
}
