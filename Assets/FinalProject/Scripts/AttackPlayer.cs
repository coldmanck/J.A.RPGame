using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour {

	PlayerHealth health;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			health = other.gameObject.GetComponent<PlayerHealth> (); 
			health.TakeDamage (30);
		}

		//Destroy(other.gameObject);
	}
}