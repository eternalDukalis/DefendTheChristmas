using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
	//STRUCTS
	public enum MoveDirection { Up, Right, Down, Left};
	[System.Serializable]
	public struct Instruction 
	{ 
		[SerializeField]
		public MoveDirection Direction;
		[SerializeField]
		public int Steps;
	}
	//VARIABLES
	public Vector2 StartPosition;
	public Instruction[] MoveInstructions;
	public GameObject PathObject;
	//STATIC VARIABLES
	static Vector2 Step;
	// Use this for initialization
	void Start () {
		Step = new Vector2 (0.1f, 0.2f);
		PlacePath ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlacePath()
	{
		Vector2 currentPosition = Step;
		currentPosition.Scale (StartPosition);
		for (int i=0; i<MoveInstructions.Length; i++)
		{
			for (int j=0; j<MoveInstructions[i].Steps; j++)
			{
				switch (MoveInstructions[i].Direction)
				{
				case MoveDirection.Up:
					currentPosition += new Vector2(0, Step.y);
					break;
				case MoveDirection.Right:
					currentPosition += new Vector2(Step.x, 0);
					break;
				case MoveDirection.Down:
					currentPosition += new Vector2(0, -Step.y);
					break;
				default:
					currentPosition += new Vector2(-Step.x, 0);
					break;
				}
				GameObject pObj = Instantiate(PathObject);
				pObj.GetComponent<RectTransform>().SetParent(this.transform, false);
				pObj.GetComponent<RectTransform>().anchorMin = currentPosition;
				pObj.GetComponent<RectTransform>().anchorMax = currentPosition + Step;
			}
		}
	}
}
