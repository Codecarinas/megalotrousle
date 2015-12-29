using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBattleController : MonoBehaviour {

	public GameObject target;
	public AudioClip damageSound;
	public AudioClip healSound;
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
		MoveToFirstButton ();
	}

	// Update is called once per frame
	void Update () {
		if (GameStateController.Instance.battleState == GameStateController.BattleState.kPlayerTurn) {
			Vector3 movement = new Vector3 (Mathf.Clamp(Input.GetAxis ("Horizontal") * 2, -1, 1), Mathf.Clamp(Input.GetAxis ("Vertical") * 2, -1, 1), 0);
			ApplyMove (movement * _speed);

			Vector3 lookatPoint = target.transform.TransformPoint (target.transform.position);

			lookatPoint.y = -lookatPoint.y;

			transform.LookAt (target.transform, transform.forward);

			Quaternion rotation = transform.rotation;
			rotation.x = 0;
			rotation.y = 0;


			transform.rotation = rotation;
		} else if (GameStateController.Instance.battleState == GameStateController.BattleState.kEnemyTurn) {
			Vector3 movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
			ApplyMove (movement * _speed);

			transform.rotation = Quaternion.identity;
		} else {
			ApplyMove (Vector2.zero);
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
		if (_buttons [0].activeInHierarchy) {
			transform.position = _buttons [0].transform.position;
			transform.rotation = Quaternion.identity;
		}
	}

	public void ApplyDamage(float damage) {
		if (damage < 0) {
			GetComponent<AudioSource> ().PlayOneShot (damageSound);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (healSound);
		}
		health += Mathf.RoundToInt(damage / defense);
	}
}
