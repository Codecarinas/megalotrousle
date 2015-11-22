using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleDialogBoxController : MonoBehaviour {
	public GameObject attackSelection;
	public GameObject dialog;
	public GameObject actSelection;
	public GameObject itemSelection;
	public GameObject actOptions;
	public Image outline;

	Image _imageComponent;
	Vector2 _originalSize;

	// Use this for initialization
	void Start () {
		_imageComponent = GetComponent<Image> ();

		_originalSize = outline.rectTransform.sizeDelta;

		HideAll ();
		EnableDialog ();

	}

	public void HideAll() {
		_imageComponent.enabled = false;
		DisableDialog ();
		DisableAttackSelection ();
		DisableActSelection ();
		DisableItemSelection ();
		DisableActOptions ();
	}

	public void EnableDialog() {
		Debug.Log ("Enabling.");
		ApplyBattleField (_originalSize);
		EnableBackground ();
		dialog.SetActive (true);
	}

	public void DisableDialog() {
		DisableBackground ();
		dialog.SetActive (false);
	}

	public void EnableItemSelection() {
		ApplyBattleField (_originalSize);
		EnableBackground ();
		itemSelection.SetActive (true);
	}

	public void DisableItemSelection() {
		DisableBackground ();
		itemSelection.SetActive (false);
	}

	public void EnableActSelection() {
		ApplyBattleField (_originalSize);
		EnableBackground ();
		actSelection.SetActive (true);
	}

	public void DisableActSelection() {
		DisableBackground ();
		actSelection.SetActive (false);
	}

	public void EnableActOptions() {
		ApplyBattleField (_originalSize);
		EnableBackground ();
		actOptions.SetActive (true);
	}

	public void DisableActOptions() {
		DisableBackground ();
		actOptions.SetActive (false);
	}

	public void EnableAttackSelection() {
		ApplyBattleField (_originalSize);
		EnableBackground ();
		attackSelection.SetActive (true);
	}

	public void DisableAttackSelection() {
		DisableBackground ();
		attackSelection.SetActive (false);
	}

	void DisableOutline() {
		outline.enabled = false;
	}

	void DisableBackground() {
		_imageComponent.enabled = false;
	}

	void EnableOutline() {
		outline.enabled = true;
	}

	void EnableBackground() {
		_imageComponent.enabled = true;
	}

	public void ApplyDialog(string dialogText) {
		dialog.GetComponent<Text> ().text = dialogText;
	}

	public void ApplyStandardBattleField() {
		ApplyBattleField (new Vector2 (200, 200));
	}

	public void ApplyBattleField(Vector2 field) {
		_targetOutlineRect = field;
	}

	Vector2 _targetOutlineRect;

	// Update is called once per frame
	void Update () {
		outline.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, Mathf.Lerp(outline.rectTransform.sizeDelta.x, _targetOutlineRect.x, Time.deltaTime * 16));
		outline.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, Mathf.Lerp(outline.rectTransform.sizeDelta.y, _targetOutlineRect.y, Time.deltaTime * 16));
	}
}
