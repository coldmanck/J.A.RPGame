using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterPlayerAttack : MonoBehaviour {

	PlayerHealth playHealth;
	MonsterHealth mHealth;

	bool isAttackPlayer = false;
	float attackTime = 0.0f;

	// Use this for initialization
	void Start () {
		mHealth = gameObject.GetComponentInParent<MonsterHealth> (); 
	}

	// Update is called once per frame
	void Update () {
		if(mHealth.monsterHealth > 0)
		if (isAttackPlayer) {
			attackTime += Time.deltaTime;

			if (attackTime >= 2.5f) {
				playHealth.TakeDamage (30);
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