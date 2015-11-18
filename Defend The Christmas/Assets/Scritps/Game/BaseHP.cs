using UnityEngine;
using System.Collections;

public class BaseHP : MonoBehaviour {

    int HealthPoints;
    Base TheBase;
	// Use this for initialization
	void Start () {
        TheBase = transform.parent.parent.gameObject.GetComponent<Base>();
        HealthPoints = TheBase.HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthPoints != TheBase.HealthPoints)
        {
            HealthPoints = TheBase.HealthPoints;
            GetComponent<RectTransform>().localScale = new Vector3(TheBase.GetHealthPercent(), 1, 1);
        }
	}
}
