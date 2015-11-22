using UnityEngine;
using System.Collections;

public class MovePlayerToMe : MonoBehaviour {
	public PlayerBattleController player;
	public Transform anchor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MovePlayer() {
		player.MoveToPosition (anchor.position);
	}
}
