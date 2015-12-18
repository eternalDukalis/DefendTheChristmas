using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Meteora : MonoBehaviour {

    static int Cost = 50;
    static int Damage = 25;
    static float Radius = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void Hit(Vector2 pos)
    {
        if (PlayerManagement.Mana < Cost)
            return;
        PlayerManagement.Mana -= Cost;
        SoundManager.Meteor();
        GameObject[] gms = GameObject.FindGameObjectsWithTag("Enemy");
        List<EnemyBehavior> objs = new List<EnemyBehavior>();
        for (int i = 0; i < gms.Length; i++)
            if ((gms[i].GetComponent<EnemyBehavior>().GetPosition() - pos).magnitude * new Vector2(Field.MapWidth, Field.MapHeight).magnitude <= Radius)
                objs.Add(gms[i].GetComponent<EnemyBehavior>());
        objs.ForEach(x => x.MeteoraEffect(Damage, 0.5f));
    }
}
