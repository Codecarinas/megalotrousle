using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewController : MonoBehaviour {

	public string vcName;


	// Use this for initialization
	protected virtual void Start () {
		vcName = ViewControllerManager.instance.RegisterViewController(this.GetType().ToString(), this);
	}
}
