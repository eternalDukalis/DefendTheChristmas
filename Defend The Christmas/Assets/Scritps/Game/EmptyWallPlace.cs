using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmptyWallPlace : MonoBehaviour {

    public enum SpellType { Wall, Nova, Meteora }
    static public SpellType Spell;
    public Vector2 Position;
    RectTransform trans;
    Button btn;
	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
        trans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Spell == SpellType.Wall)
        {
            if (WallManager.WallToPlace.GetComponent<Wall>().Cost > PlayerManagement.Mana)
                btn.interactable = false;
            else
                btn.interactable = true;
        }
        else
        {
            if (PlayerManagement.Mana < 50)
                btn.interactable = false;
            else
                btn.interactable = true;
        }
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
        switch (Spell)
        {
            case SpellType.Wall:
                PlaceWall();
                break;
            case SpellType.Nova:
                Nova.Freeze(Position);
                break;
            case SpellType.Meteora:
                Meteora.Hit(Position);
                break;
        }
    }

    void PlaceWall()
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
