using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDialogController : ViewController {
	public enum CapsOptions {
		kNone = 0,
		kToUpper,
		kToLower
	}
	public Text dialogText;
	public string text;
	public bool allCaps;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
	}

	void OnEnable() {
		dialogText.gameObject.SetActive (true);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Z)) {
			if (GameStateController.Instance.battleState == GameStateController.BattleState.kEnemyTurnIntro) {
				Debug.Log ("Switching to enemy attack.");
				dialogText.gameObject.SetActive (false);
				GameStateController.Instance.EnemyFight (5);
			}
		}
	}

	public void UpdateText(string text, CapsOptions caps, Voice voice) {
		
		switch (caps) {
		case CapsOptions.kToUpper:
			text = text.ToUpper ();
			break;
		case CapsOptions.kToLower:
			text = text.ToLower ();
			break;
		case CapsOptions.kNone:
		default:
			break;
		}

		dialogText.text = text;

		dialogText.gameObject.GetComponent<Typewriter> ().typeSound = voice;
	}
}
