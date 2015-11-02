using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	[System.Serializable]
	public struct LevelData
	{
		[SerializeField]
		public Instruction[] MoveInstructions;
		[SerializeField]
		public Vector2 StartPosition;
	}
	//VARIABLES
	public LevelData[] GameLevels;
	public GameObject PathObject;
	public Texture2D path_horizontal;
	public Texture2D[] path_corners;
	//STATIC VARIABLES
	static public Vector2 Step;
	static public int MapWidth = 14;
	static public int MapHeight = 7;
	static public Vector2 StartPosition;
	static public Instruction[] MoveInstructions;
	static int CurrentLevel = 1;
	// Use this for initialization
	void Start () {
		Step = new Vector2 ((float)1 / MapWidth, (float)1 / MapHeight);
		StartPosition = new Vector2 (GameLevels[CurrentLevel].StartPosition.x, MapHeight - GameLevels[CurrentLevel].StartPosition.y);
		MoveInstructions = GameLevels [CurrentLevel].MoveInstructions;
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
				if ((j==MoveInstructions[i].Steps-1) && (i<MoveInstructions.Length-1))
				{
					int k=1;
					if (((MoveInstructions[i].Direction==MoveDirection.Down) && (MoveInstructions[i+1].Direction==MoveDirection.Right)) || ((MoveInstructions[i].Direction==MoveDirection.Left) && (MoveInstructions[i+1].Direction==MoveDirection.Up)))
						k=0;
					if (((MoveInstructions[i].Direction==MoveDirection.Up) && (MoveInstructions[i+1].Direction==MoveDirection.Right)) || ((MoveInstructions[i].Direction==MoveDirection.Left) && (MoveInstructions[i+1].Direction==MoveDirection.Down)))
						k=1;
					if (((MoveInstructions[i].Direction==MoveDirection.Right) && (MoveInstructions[i+1].Direction==MoveDirection.Down)) || ((MoveInstructions[i].Direction==MoveDirection.Up) && (MoveInstructions[i+1].Direction==MoveDirection.Left)))
						k=2;
					if (((MoveInstructions[i].Direction==MoveDirection.Right) && (MoveInstructions[i+1].Direction==MoveDirection.Up)) || ((MoveInstructions[i].Direction==MoveDirection.Down) && (MoveInstructions[i+1].Direction==MoveDirection.Left)))
						k=3;
					pObj.GetComponent<Image>().sprite = Sprite.Create(path_corners[k], new Rect(0,0,path_horizontal.width, path_horizontal.height), new Vector2(0,0));
				}
				else
				{
					if ((MoveInstructions[i].Direction==MoveDirection.Right) || (MoveInstructions[i].Direction==MoveDirection.Left))
						pObj.GetComponent<Image>().sprite = Sprite.Create(path_horizontal, new Rect(0,0,path_horizontal.width, path_horizontal.height), new Vector2(0,0));
				}
				pObj.GetComponent<RectTransform>().SetParent(GameObject.Find("PathAll").transform, false);
				pObj.GetComponent<RectTransform>().anchorMin = currentPosition;
				pObj.GetComponent<RectTransform>().anchorMax = currentPosition + Step;
			}
		}
	}
}
