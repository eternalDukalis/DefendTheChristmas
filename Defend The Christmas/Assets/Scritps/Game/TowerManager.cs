using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {

	static public ArrayList EmptySlots;
	static public GameObject TowerToPlace;
	public GameObject EmptyPlace;
	public GameObject PlacesObject;
	float Epsilon = 0.01f;
	// Use this for initialization
	void Start () {
		MakeSlots ();
		PlacesObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public virtual void OpenTowerPlaces(GameObject currTower)
	{
		TowerToPlace = currTower;
		PlacesObject.SetActive (true);
	}

	public virtual void CloseTowerPlaces()
	{
		PlacesObject.SetActive (false);
	}

	void MakeSlots()
	{
		ArrayList path = new ArrayList (EmptySlots);
		for (int i=0; i<path.Count-1; i++)
		{
			Vector2 cur = (Vector2)path.ToArray()[i];
			Vector2[] near = new Vector2[8];
			near[0] = new Vector2(cur.x-Field.Step.x, cur.y-Field.Step.y);
			near[1] = new Vector2(cur.x, cur.y-Field.Step.y);
			near[2] = new Vector2(cur.x+Field.Step.x, cur.y-Field.Step.y);
			near[3] = new Vector2(cur.x-Field.Step.x, cur.y);
			near[4] = new Vector2(cur.x+Field.Step.x, cur.y);
			near[5] = new Vector2(cur.x-Field.Step.x, cur.y+Field.Step.y);
			near[6] = new Vector2(cur.x, cur.y+Field.Step.y);
			near[7] = new Vector2(cur.x+Field.Step.x, cur.y+Field.Step.y);
			for (int j=0; j<near.Length; j++)
				if ((!Containing(near[j])) && (IsInField(near[j])))
					EmptySlots.Add(near[j]);
		}
		for (int i=0; i<path.Count; i++)
			EmptySlots.Remove(path.ToArray()[i]);
		for (int i=0; i<EmptySlots.Count; i++)
		{
			Vector2 vec = (Vector2)EmptySlots.ToArray()[i];
			GameObject cur = Instantiate(EmptyPlace);
			cur.transform.SetParent(PlacesObject.transform, false);
			cur.GetComponent<EmptyPlace>().Set(vec);
		}
	}

	bool IsInField(Vector2 vec)
	{
		if (vec.x < 0)
			return false;
		if (vec.x >= 1)
			return false;
		if (vec.y < 0)
			return false;
		if (vec.y >= 1)
			return false;
		return true;
	}

	bool Containing(Vector2 vec)
	{
		for (int i=0; i<EmptySlots.Count; i++)
		{
			Vector2 cur = (Vector2)EmptySlots.ToArray()[i];
			if ((Mathf.Abs(cur.x-vec.x)<Epsilon) && (Mathf.Abs(cur.y-vec.y)<Epsilon))
				return true;
		}
		return false;
	}

    static public void Reset()
    {
        if (EmptySlots!=null)
            EmptySlots.Clear();
    }
}
