using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public struct Item {
	public string name;
	public string flavorText;
}

public class BattleItemSelectionController : MonoBehaviour {
	[SerializeField] private Button[] _buttonPrototypes;

	public Item[] items;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		for (int i = 0; i < _buttonPrototypes.Length; i++) {
			Text t = _buttonPrototypes [i].GetComponentInChildren<Text> ();
			if (i < items.Length) {
				t.text = items [i].name;
				_buttonPrototypes [i].gameObject.SetActive (true);
			} else {
				t.text = "";
				_buttonPrototypes [i].gameObject.SetActive (false);
			}
		}

		GetComponent<GridLayoutGroup> ().CalculateLayoutInputHorizontal ();
		GetComponent<GridLayoutGroup> ().CalculateLayoutInputVertical ();
	}
}
