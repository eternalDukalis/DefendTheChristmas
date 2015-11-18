using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

	public int HealthPoints;
	public float MoveSpeed;
	public int Damage;
	Vector2 StartPosition;
	Vector2 Step;
	Field.Instruction[] Movings;
	RectTransform itsTransform;
	Vector2 currentPosition;
	private int MaxHealthPoints;
	static float VerticalMultiplier = 1.2f;
	// Use this for initialization
	void Start () {
		itsTransform = this.GetComponent<RectTransform> ();
        itsTransform.SetParent(GameObject.Find("EnemiesAll").transform, false);
		StartPosition = Field.StartPosition;
		Step = Field.Step;
		Movings = Field.MoveInstructions;
		MaxHealthPoints = HealthPoints;
		Init ();
		StartCoroutine (Moving ());
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthPoints <= 0)
            Die();
	}

	IEnumerator Moving()
	{
		int i = 0;
		float part = 0;
		while (i < Movings.Length) 
		{
			float x = 0, y = 0, border = 0; 
			if (Movings[i].Direction==Field.MoveDirection.Right)
			{
				x = MoveSpeed;
				border = Movings[i].Steps*Step.x;
			}
			if (Movings[i].Direction==Field.MoveDirection.Left)
			{
				x = -MoveSpeed;
				border = Movings[i].Steps*Step.x;
			}
			if (Movings[i].Direction==Field.MoveDirection.Up)
			{
				y = MoveSpeed*VerticalMultiplier;
				border = Movings[i].Steps*Step.y/VerticalMultiplier;
			}
			if (Movings[i].Direction==Field.MoveDirection.Down)
			{
				y = -MoveSpeed*VerticalMultiplier;
				border = Movings[i].Steps*Step.y/VerticalMultiplier;
			}
			part += MoveSpeed;
			Move (x, y);
			if (part>border)
			{
				part = 0;
				i++;
			}
			yield return null;
		}
        GameObject.FindGameObjectWithTag("MainBase").GetComponent<Base>().HealthPoints -= Damage;
        Destroy(this.gameObject);
	}

	void Init()
	{
		currentPosition = Step;
		currentPosition.Scale (StartPosition);
		itsTransform.anchorMin = currentPosition;
		itsTransform.anchorMax = currentPosition + Step;
	}

	void Move(float x, float y)
	{
		itsTransform.anchorMin += new Vector2 (x, y);
		itsTransform.anchorMax += new Vector2 (x, y);
	}

	public Vector2 GetPosition()
	{
        if (itsTransform == null)
            return new Vector2();
		return (itsTransform.anchorMin + itsTransform.anchorMax + Field.Step) / 2;
	}

	public float GetHealthPercent()
	{
		return (float)HealthPoints / MaxHealthPoints;
	}

    void Die()
    {
        Destroy(this.gameObject);
    }
}
