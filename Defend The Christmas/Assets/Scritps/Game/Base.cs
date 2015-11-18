using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {
    public int HealthPoints;
    int MaxHealthPoints;
	// Use this for initialization
	void Start () {
        MaxHealthPoints = HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float GetHealthPercent()
    {
        return (float)HealthPoints / MaxHealthPoints;
    }
}
