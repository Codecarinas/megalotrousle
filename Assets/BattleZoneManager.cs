using UnityEngine;
using System.Collections;

public class BattleZoneManager : MonoBehaviour {
	public Vector2 targetControl;
	public float speed = 1;

	Vector2 _currentControl;

	void Start() {
		GetComponent<Animator> ().SetFloat ("X", -1);
		GetComponent<Animator> ().SetFloat ("Y", -1);
		targetControl = new Vector2 (-1, -1);
	}

	void Update() {
		_currentControl = Vector2.Lerp (_currentControl, targetControl, Time.deltaTime * speed);
		GetComponent<Animator> ().SetFloat ("X", _currentControl.x);
		GetComponent<Animator> ().SetFloat ("Y", _currentControl.y);
	}

	public void AttackZone() {
		_currentControl.x = GetComponent<Animator> ().GetFloat ("X");
		_currentControl.y = GetComponent<Animator> ().GetFloat ("Y");

		targetControl.x = 0;
		targetControl.y = 1;
	}

	public void DialogBox() {
		_currentControl.x = GetComponent<Animator> ().GetFloat ("X");
		_currentControl.y = GetComponent<Animator> ().GetFloat ("Y");

		targetControl.x = -1;
		targetControl.y = -1;
	}

	public void DefenseZone() {
		_currentControl.x = GetComponent<Animator> ().GetFloat ("X");
		_currentControl.y = GetComponent<Animator> ().GetFloat ("Y");

		targetControl.x = 1;
		targetControl.y = -1;
	}
}
