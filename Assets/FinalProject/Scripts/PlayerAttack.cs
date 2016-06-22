using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	MonsterHealth monsterHealth;
	BossHealth bossHealth;
	PlayerHealth mHealth;

	// Use this for initialization
	void Start () {
		mHealth = gameObject.GetComponentInParent<PlayerHealth> ();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		if (mHealth.health > 0) {
			if (other.tag == "monster") {
				monsterHealth = other.gameObject.GetComponent<MonsterHealth> (); 
				monsterHealth.TakeDamage (30);
			}

			if (other.tag == "boss") {
				bossHealth = other.gameObject.GetComponent<BossHealth> (); 
				bossHealth.TakeDamage (30);
			}
		}

		//Destroy(other.gameObject);
	}
}