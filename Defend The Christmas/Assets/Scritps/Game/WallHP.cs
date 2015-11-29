using UnityEngine;
using System.Collections;

public class WallHP : MonoBehaviour {

    int HealthPoints;
    Wall wl;
	// Use this for initialization
	void Start () {
        wl = transform.parent.parent.gameObject.GetComponent<Wall>();
        HealthPoints = wl.HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthPoints != wl.HealthPoints)
        {
            HealthPoints = wl.HealthPoints;
            GetComponent<RectTransform>().localScale = new Vector3(wl.GetHealthPercent(), 1, 1);
        }
	}
}
