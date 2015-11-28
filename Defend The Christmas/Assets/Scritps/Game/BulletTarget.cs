using UnityEngine;
using System.Collections;

public class BulletTarget : MonoBehaviour {

	public Vector2 Size;
	public float MoveSpeed;
	RectTransform trans;
	GameObject target;
	int damage;
	Vector2 position;
    float FreezeKoef;
    float FreezeDuration;
    bool isFrozen = false;
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
		//pos.Scale (Field.Step);
		position = pos;
		trans.anchorMin = pos - Field.Step / 2 - Size / 2;
		trans.anchorMax = pos - Field.Step / 2 + Size / 2;
		StartCoroutine (Fly ());
	}

    public void Fire(Vector2 StartPosition, GameObject Target, int Damage, float Koef, float Duration)
    {
        FreezeKoef = Koef;
        FreezeDuration = Duration;
        isFrozen = true;
        Fire(StartPosition, Target, Damage);
    }

	IEnumerator Fly()
	{
		while (true)
		{
            if (target == null)
            {
                Destroy(this.gameObject);
                break;
            }
			Vector2 mv = target.GetComponent<EnemyBehavior>().GetPosition() - position;
			if (mv.magnitude<MoveSpeed)
			{
				Destroy(this.gameObject);
                target.GetComponent<EnemyBehavior>().HealthPoints -= damage;
                if (isFrozen)
                    target.GetComponent<EnemyBehavior>().FreezeEffect(FreezeKoef, FreezeDuration);
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
