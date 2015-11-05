using UnityEngine;
using System.Collections;

public class BulletTarget : MonoBehaviour {

	public Vector2 Size;
	public float MoveSpeed;
	RectTransform trans;
	GameObject target;
	int damage;
	Vector2 position;
	// Use this for initialization
	void Start () {
		//Fire (new Vector2 (12, 5), GameObject.Find("Enemy"), 25);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(Vector2 StartPosition, GameObject Target, int Damage)
	{
		trans = this.GetComponent<RectTransform> ();
		target = Target;
		damage = Damage;
		Vector2 pos = StartPosition;
		pos.Scale (Field.Step);
		position = pos;
		trans.anchorMin = pos - Field.Step / 2 - Size / 2;
		trans.anchorMax = pos - Field.Step / 2 + Size / 2;
		StartCoroutine (Fly ());
	}

	IEnumerator Fly()
	{
		while (true)
		{
			Vector2 mv = target.GetComponent<EnemyBehavior>().GetPosition() - position;
			if (mv.magnitude<MoveSpeed)
			{
				Destroy(this.gameObject);
				break;
			}
			mv.Normalize();
			mv *= MoveSpeed;
			position += mv;
			trans.anchorMin += mv;
			trans.anchorMax += mv;
			yield return null;
		}
	}
}
