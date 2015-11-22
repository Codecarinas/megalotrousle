using UnityEngine;
using System.Collections;

public class BattleMenu : MonoBehaviour {

	public GameObject attackOptions;
	public GameObject menuOptions;
	public GameObject actOptions;
	public GameObject itemSelection;
	public GameObject attackSelection;
	public GameObject actSelection;
	public GameObject mercySelection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateController.Instance.battleState == GameStateController.BattleState.kPlayerTurn) {
			attackOptions.SetActive (true);
			menuOptions.SetActive (false);
		} else {
			attackOptions.SetActive (false);
			menuOptions.SetActive (true);
		}
	}
}
