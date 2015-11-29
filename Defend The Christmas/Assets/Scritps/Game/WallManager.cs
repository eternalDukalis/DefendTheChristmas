using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallManager : MonoBehaviour {

    public GameObject TheWall;
    public GameObject EmptyPlace;
    public GameObject PlacesObject;
    static public GameObject WallToPlace;
    static public List<Vector2> EmptySlots;
    static public GameObject stEmtpy;
	// Use this for initialization
	void Start () {
        WallToPlace = TheWall;
        stEmtpy = EmptyPlace;
        MakeSlots();
        PlacesObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void OpenWallPlaces()
    {
        PlacesObject.SetActive(true);
    }

    public virtual void CloseWallPlaces()
    {
        PlacesObject.SetActive(false);
    }

    void MakeSlots()
    {
        for (int i = 0; i < EmptySlots.Count; i++)
        {
            Vector2 vec = EmptySlots[i];
            SetSlot(vec);
        }
    }

    public void SetSlot(Vector2 pos)
    {
        GameObject cur = Instantiate(EmptyPlace);
        cur.transform.SetParent(PlacesObject.transform, false);
        cur.GetComponent<EmptyWallPlace>().Set(pos);
    }
}
