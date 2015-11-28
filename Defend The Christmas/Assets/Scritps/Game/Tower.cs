using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	public Vector2 Position;
	public float ShootingInterval;
	public float ShootingRadius;
	public int Damage;
	public GameObject Bullet;
	public int Cost;
    public bool isFrozen;
    public float FreezeKoef;
    public float FreezeDuration;
    public bool isBuffer;
    public float BuffMultiplier;
    static public Dictionary<Vector2, float> AllBuffs;
    float eps = 0.01f;
	RectTransform trans;
	Vector2 pos;
	// Use this for initialization
	void Start () {
		trans = this.GetComponent<RectTransform> ();
		//Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Inst(Vector2 pos)
	{
		Position = pos;
		Init ();
	}

	void Init()
	{
		Vector2 cur = Position;
		//cur.Scale (Field.Step);
		if (trans==null)
			trans = this.GetComponent<RectTransform> ();
		trans.anchorMin = cur;
		trans.anchorMax = cur + Field.Step;
		pos = (trans.anchorMin + trans.anchorMax) / 2;
        if (AllBuffs == null)
            AllBuffs = new Dictionary<Vector2, float>();
        if (!isBuffer)
            StartCoroutine(Shooting());
        else
            Buff();
	}

	IEnumerator Shooting()
	{
		float timeCounter = 0;
		while (true)
		{
			timeCounter += Time.deltaTime;
			if (timeCounter>=ShootingInterval)
			{
				SpawnBullet();
				timeCounter = 0;
			}
			yield return null;
		}
	}

	GameObject GetTarget()
	{
		GameObject[] gms = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject cur = null;
		float cval = 1;
		for (int i=0; i<gms.Length; i++)
		{
			EnemyBehavior eBeh = gms[i].GetComponent<EnemyBehavior>();
            //Debug.Log(eBeh.GetPosition());
            float distance = (pos - eBeh.GetPosition()).magnitude * new Vector2(Field.MapWidth, Field.MapHeight).magnitude;
			if ((distance<ShootingRadius) && (eBeh.GetHealthPercent()<=cval))
			{
				cval = eBeh.GetHealthPercent();
				cur = gms[i];
			}
		}
		return cur;
	}

	void SpawnBullet()
	{
		GameObject target = GetTarget ();
		if (target == null)
			return;
		GameObject _bullet = Instantiate (Bullet);
		_bullet.GetComponent<RectTransform> ().SetParent (GameObject.Find ("Field").GetComponent<RectTransform> (), false);
        if (isFrozen)
            _bullet.GetComponent<BulletTarget>().Fire(Position + Field.Step, target, CalcDamage(), FreezeKoef, FreezeDuration);
        else
            _bullet.GetComponent<BulletTarget>().Fire(Position + Field.Step, target, CalcDamage());
	}

    void Buff()
    {
        AddToDict(Position + new Vector2(Field.Step.x, 0), BuffMultiplier);
        AddToDict(Position + new Vector2(-Field.Step.x, 0), BuffMultiplier);
        AddToDict(Position + new Vector2(0, Field.Step.y), BuffMultiplier);
        AddToDict(Position + new Vector2(0, -Field.Step.y), BuffMultiplier);
    }

    void AddToDict(Vector2 vec, float mult)
    {
        vec = RoundVector(vec);
        if (AllBuffs.ContainsKey(vec))
            AllBuffs[vec] *= mult;
        else
            AllBuffs.Add(vec, mult);
    }

    int CalcDamage()
    {
        int res = Damage;
        Vector2 curPos = RoundVector(Position);
        if (AllBuffs.ContainsKey(curPos))
        {
            res = (int)(res * AllBuffs[curPos]);
        }
        return res;
    }

    float Round(float a)
    {
        float res = (int)a;
        float resadd = (int)((a - res) / eps);
        res += resadd * eps;
        return res;
    }

    Vector2 RoundVector(Vector2 vec)
    {
        return new Vector2(Round(vec.x), Round(vec.y));
    }
}
