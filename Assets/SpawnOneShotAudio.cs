using UnityEngine;
using System.Collections;

public class SpawnOneShotAudio : MonoBehaviour {
	public AudioClip clip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		GameObject go = new GameObject ("One Shot Audio");
		go.AddComponent<TimedDestruction> ().seconds = clip.length;
		go.AddComponent<AudioSource> ().clip = clip;
		go.GetComponent<AudioSource> ().Play ();
	}
}
