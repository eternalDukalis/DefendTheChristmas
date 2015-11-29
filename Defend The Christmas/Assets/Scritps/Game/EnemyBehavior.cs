using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

	public int HealthPoints;
	public float MoveSpeed;
	public int Damage;
    public int Drop;
    float SpeedKoef = 1;
	Vector2 StartPosition;
	Vector2 Step;
	Field.Instruction[] Movings;
	RectTransform itsTransform;
	Vector2 currentPosition;
	private int MaxHealthPoints;
	static float VerticalMultiplier = 1.2f;
    static float ShootingPeriopd = 0.5f;
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
				x = MoveSpeed*SpeedKoef;
				border = Movings[i].Steps*Step.x;
			}
			if (Movings[i].Direction==Field.MoveDirection.Left)
			{
				x = -MoveSpeed*SpeedKoef;
				border = Movings[i].Steps*Step.x;
			}
			if (Movings[i].Direction==Field.MoveDirection.Up)
			{
				y = MoveSpeed*SpeedKoef*VerticalMultiplier;
				border = Movings[i].Steps*Step.y/VerticalMultiplier;
			}
			if (Movings[i].Direction==Field.MoveDirection.Down)
			{
				y = -MoveSpeed*SpeedKoef*VerticalMultiplier;
				border = Movings[i].Steps*Step.y/VerticalMultiplier;
			}
            GameObject gm = WallNear();
            if (gm == null)
            {
                part += MoveSpeed * SpeedKoef;
                Move(x, y);
            }
            else
            {
                yield return StartCoroutine(KillWall(gm));
            }
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

    IEnumerator KillWall(GameObject target)
    {
        float per = ShootingPeriopd;
        while (target!=null)
        {
            per += Time.deltaTime;
            if (per >= ShootingPeriopd)
            {
                per = 0;
                target.GetComponent<Wall>().Hit(Damage);
            }
            yield return null;
        }
    }

    GameObject WallNear()
    {
        GameObject[] gms = GameObject.FindGameObjectsWithTag("Wall");
        GameObject cur = null;
        for (int i = 0; i < gms.Length; i++)
            if ((GetPosition()-gms[i].GetComponent<Wall>().GetPosition()).magnitude<Field.Step.magnitude)
            {
                cur = gms[i];
                break;
            }
        return cur;
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
        PlayerManagement.Gold += Drop;
        Destroy(this.gameObject);
    }

    public void FreezeEffect(float koef, float duration)
    {
        StartCoroutine(Freeze(koef, duration));
    }

    IEnumerator Freeze(float koef, float duration)
    {
        SpeedKoef = koef;
        float tm = 0;
        while (tm < duration)
        {
            tm += Time.deltaTime;
            yield return null;
        }
        SpeedKoef = 1;
    }
}
