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
    public struct Wave
    {
        public GameObject Unit;
        public int Count;
    }
	[System.Serializable]
	public struct LevelData
	{
		[SerializeField]
		public Instruction[] MoveInstructions;
        [SerializeField]
        public Wave[] Waves;
		[SerializeField]
		public Vector2 StartPosition;
	}
	//VARIABLES
	public LevelData[] GameLevels;
	public GameObject PathObject;
	public Texture2D path_horizontal;
	public Texture2D[] path_corners;
    public GameObject BaseObject;
	//STATIC VARIABLES
	static public Vector2 Step;
	static public int MapWidth = 14;
	static public int MapHeight = 7;
	static public Vector2 StartPosition;
	static public Instruction[] MoveInstructions;
	static public int CurrentLevel = 1;
    static public float UnitInterval = 0.2f;
    static public float WaveInterval = 2;
    static public int CurrentWave = 0;
    static Vector2 BaseSize;
	// Use this for initialization
	void Start () {
		Step = new Vector2 ((float)1 / MapWidth, (float)1 / MapHeight);
        BaseSize = new Vector2(2, 2);
		StartPosition = new Vector2 (GameLevels[CurrentLevel].StartPosition.x, MapHeight - GameLevels[CurrentLevel].StartPosition.y);
		MoveInstructions = GameLevels [CurrentLevel].MoveInstructions;
		PlacePath ();
        StartCoroutine(EnemiesEmission());
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
        switch (MoveInstructions[MoveInstructions.Length-1].Direction)
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
        GameObject bObj = Instantiate(BaseObject);
        RectTransform bTrans = bObj.GetComponent<RectTransform>();
        bTrans.SetParent(GameObject.Find("Field").transform, false);
        bTrans.anchorMin = currentPosition + new Vector2(0, Step.y) / 2 - new Vector2(0, Step.y * BaseSize.y / 2);
        bTrans.anchorMax = currentPosition + new Vector2(0, Step.y) / 2 + new Vector2(Step.x * BaseSize.x, Step.y * BaseSize.y / 2);
	}

    IEnumerator EnemiesEmission()
    {
        float ut = 0, wt = 0;
        int UnitsEmissed = 0;
        while (CurrentWave < GameLevels[CurrentLevel].Waves.Length)
        {
            ut += Time.deltaTime;
            wt += Time.deltaTime;
            if ((ut > UnitInterval) && (wt > WaveInterval))
            {
                Instantiate(GameLevels[CurrentLevel].Waves[CurrentWave].Unit);
                UnitsEmissed++;
                ut = 0;
                if (UnitsEmissed >= GameLevels[CurrentLevel].Waves[CurrentWave].Count)
                {
                    CurrentWave++;
                    UnitsEmissed = 0;
                    wt = 0;
                }
            }
            yield return null;
        }
    }
}
