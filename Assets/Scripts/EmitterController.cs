using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EmitterController : MonoBehaviour {

	[System.Serializable]
	public struct Attack {
		public GameObject attackObject;
		public KeyCode attackKey;
		public float magicCost;
		public float rateOfFire;
		public float timeTillNextFire;
	}

	[SerializeField] private Attack[] _attacks = new Attack[5];
	[SerializeField] private Slider _magicSlider;
	[SerializeField] private Text _magicText;
	[SerializeField] private Text[] _attackTexts;
	public float maxMagic = 100;
	public float magic = 100;
	public float magicRegenRate;

	private float _timeTillNextRegen = 0;

	// Use this for initialization
	void Start () {
		_magicSlider.maxValue = maxMagic;
		for (int i = 0; i < _attacks.Length; i++) {
			if (_attacks [i].attackObject != null) {
				_attackTexts [i].text += " " + _attacks [i].attackObject.name + " - " + _attacks[i].magicCost + "MP";
				_attackTexts [i].gameObject.SetActive (true);
			}
		}

		_timeTillNextRegen = (1 / magicRegenRate);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateController.Instance.gameState == GameStateController.GameState.kFight && GameStateController.Instance.battleState == GameStateController.BattleState.kPlayerTurn) {
			if (magic > 0) {
				if (Input.GetKey (KeyCode.Z)) {
					EnableAttack (0);
				}

				if (Input.GetKey (KeyCode.X)) {
					EnableAttack (1);
				}

				if (Input.GetKey (KeyCode.C)) {
					EnableAttack (2);
				}

				if (Input.GetKey (KeyCode.V)) {
					EnableAttack (3);
				}

				if (Input.GetKey (KeyCode.Space)) {
					EnableAttack (4);
				}
			}

			{
				int i = 0;
				while (i < _attacks.Length) {
					if (_attacks [i].timeTillNextFire > 0) {
						_attacks [i].timeTillNextFire -= Time.deltaTime;
					}
					i++;
				}
			}
		}

		if (magic < maxMagic && _timeTillNextRegen < 0) {
			magic += 1;
			_timeTillNextRegen = (1 / magicRegenRate);
		}

		_magicSlider.value = magic;
		_magicText.text = magic.ToString() + "/" + maxMagic.ToString();

		_timeTillNextRegen -= Time.deltaTime;
	}

	void EnableAttack(int key) {
		if (_attacks[key].attackObject != null && (magic - _attacks[key].magicCost) > -1 && _attacks[key].timeTillNextFire <= 0) {
			((GameObject)Instantiate (_attacks[key].attackObject, transform.position, transform.rotation)).SetActive (true);
			magic -= _attacks[key].magicCost;

			_attacks[key].timeTillNextFire = 1 / _attacks[key].rateOfFire;

			_timeTillNextRegen = (1 / magicRegenRate);
		}
	}
}
