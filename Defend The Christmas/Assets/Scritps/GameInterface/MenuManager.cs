using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject CurrentMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void GoTo(GameObject target)
	{
		CurrentMenu.SetActive (false);
		CurrentMenu = target;
		target.SetActive (true);
	}
}
