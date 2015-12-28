using UnityEngine;
using System.Collections;

public class BackgroundAudioController : MonoBehaviour {

	[System.Serializable]
	public class BGAudio {
		public AudioClip audioClip;
		public bool loop;
		public float earlyCutoff;
	}

	public BGAudio[] audioClips;

	public int _index = 0;

	BGAudio _queuedClip = null;

	AudioSource _audioSource = null;
	AudioSource audioSource {
		get {
			if (_audioSource == null) {
				_audioSource = GetComponent<AudioSource> ();
				if (_audioSource == null) {
					_audioSource = gameObject.AddComponent<AudioSource> ();
				}
			}

			return _audioSource;
		}
	}

	Timer _timer = null;
	Timer timer {
		get {
			if (_timer == null) {
				_timer = gameObject.AddComponent<Timer> ();
			}

			return _timer;
		}
	}

	public float _cutoff;
	public void PlayNextClip() {
		if (_index < audioClips.Length) {
			_cutoff = float.MaxValue;
			if (audioClips [_index].earlyCutoff > 0) {
				_cutoff = audioSource.clip.length - audioClips [_index].earlyCutoff;
			}
			_index++;
			GetComponent<AudioSource> ().loop = false;
			_queuedClip = audioClips [_index];
		}
	}
	public bool _skip = false;
	public void RequestSkip() {
		_skip = true;
	}

	// Use this for initialization
	void Start () {
		PlayNextClip ();
		timer.timerCallbacks.AddListener (PlayNextClip);
	}
	public float currentTime;
	// Update is called once per frame
	void Update () {
		currentTime = audioSource.time;
		if (_queuedClip != null) {
			if (!audioSource.isPlaying || (audioSource.isPlaying && _cutoff < audioSource.time)) {
				// If we ever get here when a skip has been requested, we need to do something a bit special.
				if (_skip) {
					Debug.Log ("AAAAAAAA");
				} else {
					audioSource.clip = _queuedClip.audioClip;
					audioSource.loop = _queuedClip.loop;
					audioSource.Play ();
					if (!_queuedClip.loop) {
						timer.timeRemaining = _queuedClip.audioClip.length - _queuedClip.earlyCutoff;
						timer.StartTimer ();
						timer.destroyMe = false;
					}
					_queuedClip = null;
				}
			}
		}

		if (_skip) {
			_cutoff = audioClips [_index].audioClip.length - audioClips [_index].earlyCutoff;
			timer.timeRemaining = audioClips [_index].audioClip.length - audioSource.time - audioClips [_index].earlyCutoff;
			timer.StartTimer ();
			_skip = false;
		}
	}
}
