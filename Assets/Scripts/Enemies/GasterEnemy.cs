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
		
	}

	public override void CycleDefenseDialog() {
		base.CycleDefenseDialog ();
	}
}
