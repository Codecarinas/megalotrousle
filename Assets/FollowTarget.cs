using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public GameObject target;
	public float followSpeed;
	// Use this for initialization
	void Start () {
	
	}

	Vector3 _previousPoint;
	
	// Update is called once per frame
	void Update () {
		Vector3 lookatPoint = target.transform.position;

		lookatPoint = Vector3.Lerp (_previousPoint, lookatPoint, Time.deltaTime * followSpeed);
		//lookatPoint.y = -lookatPoint.y;

		transform.LookAt (lookatPoint);

		Quaternion rotation = transform.rotation;
		rotation.x = 0;
		rotation.y = 0;

		rotation *= Quaternion.Euler (new Vector3 (0, 0, 180));

		transform.rotation = rotation;
		_previousPoint = lookatPoint;
	}
}
