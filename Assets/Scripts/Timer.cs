using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Timer : MonoBehaviour {
	/// <summary>
	/// The time remaining.
	/// </summary>
	public float timeRemaining = 1.0f;

	/// <summary>
	/// Determines if this object will be destroyed upon completion of the timer.  Use with care.
	/// </summary>
	public bool destroyMe = false;

	/// <summary>
	/// The callbacks that are executed when timeRemaining reaches zero.
	/// </summary>
	public UnityEvent timerCallbacks = new UnityEvent();

	/// <summary>
	/// Determines if the timer is running or not.  Use StartTimer, PauseTimer, and RestartTimer methods below to control the timer.
	/// </summary>
	public bool running {
		get {
			return _running;
		}
	}

	private bool _running = false;
	private float _originalTime = 0.0f;

	public void StartTimer() {
		_running = true;
	}

	public void StartTimer(float time) {
		_originalTime = time;
		timeRemaining = time;
		_running = true;
	}

	public void PauseTimer() {
		_running = false;
	}

	public void RestartTimer() {
		timeRemaining = _originalTime;
		_running = true;
	}

	// Use this for initialization
	void Start () {
		_originalTime = timeRemaining;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeRemaining > 0 && _running) {
			timeRemaining -= Time.deltaTime;
		} else {
			if (_running && timeRemaining <= 0.0f) {
				_running = false;
				timerCallbacks.Invoke ();
				if (destroyMe) {
					Destroy (gameObject);
				}
			}
		}
			
	}
}
