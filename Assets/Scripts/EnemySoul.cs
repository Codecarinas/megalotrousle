using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySoul : MonoBehaviour {
	public float maxHealth = 100.0f;
	public float health = 100.0f;
	public float displayHealthFor = 3.0f;
	public Slider healthBar;
	public Text damageText;
	public AudioClip deathSound;
	public AudioClip damageSound;

	private float timeTillRemove;
	private Vector3 _damageTextOrigin;

	// Use this for initialization
	void Start () {
		healthBar.maxValue = maxHealth;
		healthBar.value = health;

		healthBar.gameObject.SetActive (false);
		damageText.gameObject.SetActive (false);
		_damageTextOrigin = damageText.transform.localPosition;
	}
	// Update is called once per frame
	void Update () {
		if (timeTillRemove <= 0) {
			healthBar.gameObject.SetActive (false);
			damageText.gameObject.SetActive (false);
			damageText.transform.localPosition = _damageTextOrigin;
		} else {
			timeTillRemove -= Time.deltaTime;
			Vector3 tempOrigin = _damageTextOrigin;
			tempOrigin.x += Mathf.Sin (timeTillRemove * 64) * 4;
			damageText.transform.localPosition = tempOrigin;
		}

		Vector3 pos = transform.localPosition;
		pos.x = Random.Range (-1.7f, 1.7f);
		pos.y = Random.Range (-1.7f, 1.7f);

		transform.localPosition = Vector3.MoveTowards (transform.localPosition, pos, Time.deltaTime * 16);
	}

	public void ApplyDamage(float damage) {
		health += damage;
		healthBar.value = health;

		if (damage < 0) {
			damageText.color = Color.red;
		} else {
			damageText.color = Color.green;
		}

		damageText.text = damage.ToString ();

		damageText.gameObject.SetActive (true);
		healthBar.gameObject.SetActive (true);

		timeTillRemove = displayHealthFor;

		if (health < 1) {
			GetComponent<AudioSource> ().clip = deathSound;
			GetComponent<AudioSource> ().Play ();
		} else {
			GetComponent<AudioSource> ().clip = damageSound;
			GetComponent<AudioSource> ().Play ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.GetComponent<FriendlinessPellet> () != null) {
			FriendlinessPellet pellet = coll.collider.GetComponent<FriendlinessPellet> ();
			ApplyDamage (pellet.damage);
		}
	}
}
