using UnityEngine;
using System.Collections;

public class PlayerBattleController : MonoBehaviour {

	public GameObject target;
	[SerializeField] private float _speed = 2;
	[SerializeField] private Transform _startTransform;
	[SerializeField] private GameObject[] _buttons;

	// Use this for initialization
	void Start () {
	
	}

	int _buttonIndex = 0;

	// Update is called once per frame
	void Update () {
		if (GameStateController.Instance.battleState == GameStateController.BattleState.kPlayerTurn) {
			Vector3 tempPos = transform.localPosition;
			tempPos.x += Input.GetAxis ("Horizontal") * Time.deltaTime * _speed;
			tempPos.y += Input.GetAxis ("Vertical") * Time.deltaTime * _speed;

			transform.localPosition = tempPos;
			Vector3 targetVec = target.transform.position - transform.position;
			targetVec.Normalize ();
			Quaternion rotation = Quaternion.LookRotation (targetVec);
			rotation.x = 0;
			rotation.y = 0;

			if (targetVec.x <= 0) {
				rotation *= Quaternion.Euler (new Vector3 (0, 0, 180));
			}

			rotation *= Quaternion.Euler (new Vector3 (0, 0, 90));

			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 4);
		}
	}

	public void MoveToStart() {
		transform.position = _startTransform.position;
		transform.rotation = _startTransform.rotation;
	}

	public void MoveToPosition(Vector3 position) {
		transform.position = position;
		transform.rotation = Quaternion.identity;
	}
}
