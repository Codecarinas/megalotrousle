using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

	public enum Direction {
		X = 0,
		Y,
		Z
	};

	public Direction direction;
	public float speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		switch (direction) {
		case Direction.X:
			
			break;
		case Direction.Y:
			break;
		case Direction.Z:
			break;
		}
	
	}
}
