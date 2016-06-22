using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossAttackPlayer : MonoBehaviour {

	PlayerHealth playHealth;
	BossHealth mHealth;

	bool isAttackPlayer = false;
	float attackTime = 0.0f;

	// Use this for initialization
	void Start () {
		mHealth = gameObject.GetComponentInParent<BossHealth> (); 
	}

	// Update is called once per frame
	void Update () {
		if(mHealth.bossHealth > 0)
		if (isAttackPlayer) {
			attackTime += Time.deltaTime;

			if (attackTime >= 1.0f) {
				playHealth.TakeDamage (100);
				attackTime = 0.0f;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			isAttackPlayer = true;
			playHealth = other.gameObject.GetComponent<PlayerHealth> (); 
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			attackTime = 0.0f;
			isAttackPlayer = false;
		}
	}
}