using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Typewriter : MonoBehaviour {
	public AudioClip typeSound;
	public string textToType; 
	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().clip = typeSound;
	}
	
	// Update is called once per frame

	int _chatterCounter = 0;
	char _charToAdd;
	void Update () {
		if (textToType.Length > 0) {
			_chatterCounter++;
			if (_charToAdd == '.') {
				if (_chatterCounter > 32) {
					_charToAdd = textToType.ToCharArray () [0];
					textToType = textToType.Remove (0, 1);
					GetComponent<Text> ().text += _charToAdd.ToString ();
					_chatterCounter = 0;
				}
			} else {
				_charToAdd = textToType.ToCharArray () [0];
				textToType = textToType.Remove (0, 1);
				GetComponent<Text> ().text += _charToAdd.ToString ();

				if ((_chatterCounter % 4) == 0) {
					GetComponent<AudioSource> ().Play ();
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			if (textToType == "") {
				if (GameStateController.Instance.gameState == GameStateController.GameState.kFight) {
					//GameStateController.Instance.PlayerFight (0);
				}
			}
			GetComponent<Text> ().text += textToType;
			textToType = "";
		}
	}

	void OnEnable() {
		textToType = GetComponent<Text> ().text;
		GetComponent<Text> ().text = "";
	}

	void OnDisable() {
		GetComponent<Text> ().text += textToType;
	}
}
