using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour {

    public float AnimInterval;
    public Texture2D[] Elements;
    Image img;
    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Animate()
    {
        int it = 0;
        float tm = 0;
        while (true)
        {
            tm += Time.deltaTime;
            if (tm > AnimInterval)
            {
                tm = 0;
                it++;
                if (it == Elements.Length)
                    it = 0;
                Sprite nS = Sprite.Create(Elements[it], new Rect(0, 0, Elements[it].width, Elements[it].height), new Vector2(0, 0));
                img.sprite = nS;
            }
            yield return null;
        }
    }
}
