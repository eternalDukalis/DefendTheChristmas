using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmptyPlace : MonoBehaviour {

	public Vector2 Position;
	RectTransform trans;
    Button btn;
	// Use this for initialization
	void Start () {
		trans = GetComponent<RectTransform> ();
        btn = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (TowerManager.TowerToPlace.GetComponent<Tower>().Cost > PlayerManagement.Gold)
            btn.interactable = false;
        else
            btn.interactable = true;
	}

	public void Set(Vector2 pos)
	{
		Position = pos;
		if (trans==null)
			trans = GetComponent<RectTransform> ();
		trans.anchorMin = Position;
		trans.anchorMax = Position + Field.Step;
	}

    public virtual void Place()
    {
        if (TowerManager.TowerToPlace.GetComponent<Tower>().Cost > PlayerManagement.Gold)
            return;
        GameObject tw = Instantiate(TowerManager.TowerToPlace);
        tw.transform.SetParent(GameObject.Find("TowersAll").transform, false);
        tw.GetComponent<Tower>().Inst(Position);
        PlayerManagement.Gold -= TowerManager.TowerToPlace.GetComponent<Tower>().Cost;
        //GameObject.Find ("ControlPanel").GetComponent<TowerManager> ().CloseTowerPlaces ();
        //GameObject.Find ("ControlPanel").GetComponent<MenuManager> ().GoTo (GameObject.Find ("MainScreen"));
        Destroy(this.gameObject);
    }
}
