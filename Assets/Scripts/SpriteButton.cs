using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpriteButton : MonoBehaviour {

	public UnityEvent selectCallbacks = new UnityEvent();
	public UnityEvent hoverCallbacks = new UnityEvent();

	private bool _playerSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.Z) && _playerSelected) {
			selectCallbacks.Invoke ();
		}*/
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.GetComponent<EmitterController> () != null) {
			_playerSelected = true;
			hoverCallbacks.Invoke ();
		}
	}

	/*
	void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.GetComponent<EmitterController> () != null) {
			_playerSelected = true;
			hoverCallbacks.Invoke ();
		}
	}
	*/

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.GetComponent<EmitterController> () != null) {
			_playerSelected = false;
		}
	}
}
