using UnityEngine;
using System.Collections;

public class FriendlinessPellet : MonoBehaviour {
	public float damage = 1;
	public bool applyKarmaRedemption = false;
	public bool destroyOnCollision = true;

	void Start() {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (destroyOnCollision) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<EnemySoul> () != null) {
			EnemySoul s = other.GetComponent<EnemySoul> ();
			s.ApplyDamage (damage);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.GetComponent<EnemySoul> () != null && applyKarmaRedemption) {
			EnemySoul s = other.GetComponent<EnemySoul> ();
			s.ApplyDamage (damage);
		}
	}
}
