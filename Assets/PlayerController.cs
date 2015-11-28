using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float playerSpeed = 0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		GetComponent<Prime31.CharacterController2D> ().move (movement * playerSpeed);
	}
}
