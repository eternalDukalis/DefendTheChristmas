using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour {

    int HealthPoints;
    EnemyBehavior EB;
	// Use this for initialization
	void Start () {
        EB = transform.parent.parent.gameObject.GetComponent<EnemyBehavior>();
        HealthPoints = EB.HealthPoints;
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthPoints != EB.HealthPoints)
        {
            HealthPoints = EB.HealthPoints;
            GetComponent<RectTransform>().localScale = new Vector3(EB.GetHealthPercent(), 1, 1);
        }
	}
}
