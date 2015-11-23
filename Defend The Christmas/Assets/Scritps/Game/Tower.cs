using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public Vector2 Position;
	public float ShootingInterval;
	public float ShootingRadius;
	public int Damage;
	public GameObject Bullet;
	public int Cost;
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
		StartCoroutine (Shooting ());
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
		_bullet.GetComponent<BulletTarget> ().Fire (Position + Field.Step, target, 25);
	}
}
