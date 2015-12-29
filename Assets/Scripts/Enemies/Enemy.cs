using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Voice {
	public AudioClip[] vowel;
	public AudioClip[] constants;
	public AudioClip[] punctuation;
}

public class Enemy : MonoBehaviour {
	public int health;
	public int attack;
	public int defense;
	public float attackTime;
	public Voice voice;
	public EnemySoul soul;
	public GameObject[] attacks;
	public List<Acts> actions;
	public Acts attackAction;
	public Response[] dialogs;
	public bool useMainResponsesDefense = false;
	public bool useMainResponsesAttack = false;
	public bool randomizeAttacks = false;
	public bool randomizeDialogs = false;
	public EnemyDialogController dialogPrototype;

	[System.Serializable]
	public class Response {
		public string responseText;
		public EnemyDialogController.CapsOptions capsOptions;
		public UnityEvent callbacks = new UnityEvent();
	}

	[System.Serializable]
	public class Acts
	{
		public string action;
		public Response[] responses;
		public bool randomizeResponses;

		private int _responseIndex = 0;

		public int currentResponseIndex {
			get {
				return _responseIndex;
			}
		}

		public Response NextResponse() {
			if (randomizeResponses) {
				return responses [Random.Range (0, responses.Length - 1)];
			}

			Response res = responses [_responseIndex];
			_responseIndex++;
			return res;
		}
	}

	// Use this for initialization
	protected virtual void Start () {

		NotificationCenter.defaultInstance.AddObserver ("GameState.Dialog.Exit", (string notification) => {
			RemoveDialog();
		});

		soul.maxHealth = health;
		soul.health = health;
	}

	protected int _responseIndex = 0;
	public virtual void CycleDefenseDialog() {
		Response res = new Response { responseText = "...", capsOptions = EnemyDialogController.CapsOptions.kNone };
		if (useMainResponsesDefense) {
			if (randomizeDialogs) {
				res = dialogs [Random.Range (0, dialogs.Length - 1)];
			} else {
				res = dialogs [Mathf.Clamp (_responseIndex, 0, dialogs.Length - 1)];
				_responseIndex++;
			}
		} else {
			res = attackAction.NextResponse ();
		}

		dialogPrototype.UpdateText (res.responseText, res.capsOptions, voice);

		res.callbacks.Invoke ();
	}

	public virtual void CycleDefenseDialog(int actIndex) {
		Response res = new Response { responseText = "...", capsOptions = EnemyDialogController.CapsOptions.kNone };
		if (useMainResponsesDefense) {
			if (randomizeDialogs) {
				res = dialogs [Random.Range (0, dialogs.Length - 1)];
			} else {
				res = dialogs [Mathf.Clamp (_responseIndex, 0, dialogs.Length - 1)];
				_responseIndex++;
			}
		} else {
			res = actions[actIndex].NextResponse ();
		}

		dialogPrototype.UpdateText (res.responseText, res.capsOptions, voice);
	}

	public void DisplayDialog() {
		dialogPrototype.gameObject.SetActive (true);
	}

	public void RemoveDialog() {
		dialogPrototype.gameObject.SetActive (false);
	}

	protected int _attackIndex;
	public virtual void CycleAttack() {
		RemoveDialog ();
		if (randomizeAttacks) {
			_attackIndex = Random.Range (0, attacks.Length - 1);
		} else {
			if (_attackIndex < attacks.Length - 1) {
				_attackIndex++;
			}
		}

		GameObject go = GameObject.Instantiate (attacks [_attackIndex]);
		go.layer = 12;
		int children = go.transform.childCount;
		for (int i = 0; i < children; i++) {
			go.transform.GetChild (i).gameObject.layer = 12;
		}
	}
}
