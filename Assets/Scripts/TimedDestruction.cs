using UnityEngine;
using System.Collections;

public class TimedDestruction : MonoBehaviour {
	public float seconds = 5;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (seconds <= 0) {
			Destroy (gameObject);
		}
		seconds -= Time.fixedDeltaTime;
	}

	void ManualOverride() {
		Destroy (gameObject);
	}
}
