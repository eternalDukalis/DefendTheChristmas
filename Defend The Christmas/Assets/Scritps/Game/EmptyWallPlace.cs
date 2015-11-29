using UnityEngine;
using System.Collections;

public class EmptyWallPlace : MonoBehaviour {

    public Vector2 Position;
    RectTransform trans;
	// Use this for initialization
	void Start () {
        trans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Set(Vector2 pos)
    {
        Position = pos;
        if (trans == null)
            trans = GetComponent<RectTransform>();
        trans.anchorMin = Position;
        trans.anchorMax = Position + Field.Step;
    }

    public virtual void Place()
    {
        if (WallManager.WallToPlace.GetComponent<Wall>().Cost > PlayerManagement.Mana)
            return;
        GameObject wl = Instantiate(WallManager.WallToPlace);
        wl.transform.SetParent(GameObject.Find("TowersAll").transform, false);
        wl.GetComponent<Wall>().Inst(Position);
        PlayerManagement.Mana -= WallManager.WallToPlace.GetComponent<Wall>().Cost;
        Destroy(this.gameObject);
    }
}
