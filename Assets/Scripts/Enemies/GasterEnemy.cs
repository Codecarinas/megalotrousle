using UnityEngine;
using System.Collections;

public class GasterEnemy : Enemy {

	public ParticleSystem gasterHaze;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateController.Instance.battleState == GameStateController.BattleState.kEnemyTurnIntro) {
			GetComponent<Animator> ().SetFloat ("Talking", 1);
		} else {
			GetComponent<Animator> ().SetFloat ("Talking", 0);
		}
	}

	public override void CycleDefenseDialog() {
		base.CycleDefenseDialog ();
	}
}
