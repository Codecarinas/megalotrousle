using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBattleController : MonoBehaviour {

	public GameObject target;
	public float health;
	public float maxHealth;
	public int attack = 1;
	public int defense = 1;
	public Slider healthBar;
	public Text healthText;
	[SerializeField] private float _speed = 2;
	[SerializeField] private Transform _startTransform;
	[SerializeField] private GameObject[] _buttons;

	// Use this for initialization
	void Start () {
		healthBar.maxValue = maxHealth;
	}

	// Update is called once per frame
	void Update () {
		if (GameStateController.Instance.battleState == GameStateController.BattleState.kPlayerTurn || GameStateController.Instance.battleState == GameStateController.BattleState.kEnemyTurn) {
			Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
			ApplyMove (movement * _speed);

			Vector3 lookatPoint = target.transform.position;

			lookatPoint.y = -lookatPoint.y;

			transform.LookAt (lookatPoint);

			Quaternion rotation = transform.rotation;
			rotation.x = 0;
			rotation.y = 0;

			rotation *= Quaternion.Euler (new Vector3 (0, 0, 180));

			transform.rotation = rotation;
		}

		healthBar.value = health;
		healthText.text = health.ToString () + "/" + maxHealth.ToString ();
	}

	void ApplyMove(Vector2 direction) {
		GetComponent<Rigidbody2D> ().velocity = direction;
	}

	public void MoveToStart() {
		transform.position = _startTransform.position;
		transform.rotation = _startTransform.rotation;
	}

	public void MoveToPosition(Vector3 position) {
		transform.position = position;
		transform.rotation = Quaternion.identity;
	}

	public void MoveToFirstButton() {
		transform.position = _buttons [0].transform.position;
		transform.rotation = Quaternion.identity;
	}

	public void ApplyDamage(float damage) {
		health += Mathf.RoundToInt(Mathf.Clamp(damage / defense, 1, int.MaxValue));
	}
}
