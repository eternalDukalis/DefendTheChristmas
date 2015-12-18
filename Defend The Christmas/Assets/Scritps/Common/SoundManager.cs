using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip newWave;
    public AudioClip bangBase;
    public AudioClip destructionWall;
    public AudioClip meteor;
    public AudioClip monsterDeath;
    public AudioClip nova;
    static SoundManager SM;
	// Use this for initialization
	void Start () {
        SM = FindObjectOfType<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void NewWave()
    {
        if (!Settings.SoundOn)
            return;
        GameObject go = new GameObject("newWave", typeof(AudioSource));
        go.GetComponent<AudioSource>().clip = SM.newWave;
        SM.StartCoroutine(SM.WaitSound(go));
    }
    static public void BangBase()
    {
        if (!Settings.SoundOn)
            return;
        GameObject go = new GameObject("bangBase", typeof(AudioSource));
        go.GetComponent<AudioSource>().clip = SM.bangBase;
        SM.StartCoroutine(SM.WaitSound(go));
    }
    static public void DestructionWall()
    {
        if (!Settings.SoundOn)
            return;
        GameObject go = new GameObject("destructionWall", typeof(AudioSource));
        go.GetComponent<AudioSource>().clip = SM.destructionWall;
        SM.StartCoroutine(SM.WaitSound(go));
    }
    static public void Meteor()
    {
        if (!Settings.SoundOn)
            return;
        GameObject go = new GameObject("meteor", typeof(AudioSource));
        go.GetComponent<AudioSource>().clip = SM.meteor;
        SM.StartCoroutine(SM.WaitSound(go));
    }
    static public void MonsterDeath()
    {
        if (!Settings.SoundOn)
            return;
        GameObject go = new GameObject("monsterDeath", typeof(AudioSource));
        go.GetComponent<AudioSource>().clip = SM.monsterDeath;
        SM.StartCoroutine(SM.WaitSound(go));
    }
    static public void Nova()
    {
        if (!Settings.SoundOn)
            return;
        GameObject go = new GameObject("nova", typeof(AudioSource));
        go.GetComponent<AudioSource>().clip = SM.nova;
        SM.StartCoroutine(SM.WaitSound(go));
    }

    IEnumerator WaitSound(GameObject go)
    {
        AudioSource AS = go.GetComponent<AudioSource>();
        AS.Play();
        while (AS.isPlaying)
            yield return null;
        Destroy(AS.gameObject);
    }
}
