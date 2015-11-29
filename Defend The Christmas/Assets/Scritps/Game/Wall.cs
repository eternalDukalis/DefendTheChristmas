using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public int HealthPoints;
    public Vector2 Position;
    public int Cost;
    int MaxHealthPoints;
    RectTransform trans;
	// Use this for initialization
	void Start () {
        MaxHealthPoints = HealthPoints;
        //Init();
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthPoints <= 0)
            Die();
	}

    public void Inst(Vector2 pos)
    {
        Position = pos;
        Init();
    }

    void Init()
    {
        Vector2 cur = Position;
        //cur.Scale(Field.Step);
        if (trans == null)
            trans = this.GetComponent<RectTransform>();
        trans.anchorMin = cur;
        trans.anchorMax = cur + Field.Step;
    }

    public float GetHealthPercent()
    {
        return (float)HealthPoints / MaxHealthPoints;
    }

    public Vector2 GetPosition()
    {
        if (trans == null)
            return new Vector2();
        return (trans.anchorMin + trans.anchorMax + Field.Step) / 2;
    }

    public void Hit(int damage)
    {
        HealthPoints -= damage;
    }

    void Die()
    {
        GameObject.Find("ControlPanel").GetComponent<WallManager>().SetSlot(Position);
        Destroy(this.gameObject);
    }
}
