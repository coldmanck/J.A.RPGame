using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {

	public float monsterHealth;
	private float deathTime;
	public GameObject healthBar;
	PlayerHealth playerHealth;

	GameObject player; 

	// Use this for initialization
	void Start () {
		monsterHealth = 100f;
		deathTime = 0f;

		player = GameObject.FindGameObjectWithTag ("Player");

		playerHealth = player.GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(monsterHealth <= 0.0)
			deathTime += Time.deltaTime;

		if (deathTime >= 10)
			Destroy (this.gameObject);

		if(playerHealth.health <= 0)
			monsterHealth = 100f;

		SetHealthBar ();
	}

	public void TakeDamage (float amount)
	{
		// Decrement the player's health by amount.
		if (monsterHealth - amount < 0)
			monsterHealth = 0;
		else
			monsterHealth -= amount;
	}

	void SetHealthBar (){
		float barLenth = 1f * monsterHealth / 100.0f;
		healthBar.transform.localScale = new Vector3 (barLenth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}