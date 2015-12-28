using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewControllerManager : MonoBehaviour {

	private static ViewControllerManager _instance = null;

	public static ViewControllerManager instance {
		get {
			if (_instance == null)
			{
				_instance = new GameObject("VC Manager", typeof(ViewControllerManager)).GetComponent<ViewControllerManager>();
			}
			return _instance;
		}
	}

	void OnDestroy()
	{
		_instance = null;
	}

	Dictionary<string, ViewController> _viewControllers = new Dictionary<string, ViewController>();

	public ViewController this[string key] {
		get {
			ViewController retVal = null;

			if (_viewControllers.ContainsKey(key))
			{
				retVal = _viewControllers[key];
			}

			return retVal;
		}
	}

	public string RegisterViewController(string vcName, ViewController vc)
	{
		if (_viewControllers.ContainsKey(vcName))
		{
			int vcCount = 1;
			while(_viewControllers.ContainsKey(vcName + vcCount.ToString()))
			{
				vcCount++;
			}
			vcName = vcName + vcCount.ToString();
		}

		_viewControllers[vcName] = vc;

		return vcName;
	}

	public void RemoveViewController(string vcName)
	{
		_viewControllers.Remove(vcName);
	}

	public void Clear() {
		_viewControllers.Clear ();
	}

	// Use this for initialization
	void Start () {
		if (this != _instance && _instance != null)
		{
			DestroyImmediate(gameObject);
		}

		_instance = this;
	}

	// Update is called once per frame
	void Update () {

	}
}
