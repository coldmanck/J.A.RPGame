using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public float bossHealth;
	private float deathTime;
	public GameObject healthBar;
	PlayerHealth playerHealth;
	public GameObject winScreen;

	GameObject player; 

	// Use this for initialization
	void Start () {
		bossHealth = 500f;
		deathTime = 0f;

		player = GameObject.FindGameObjectWithTag ("Player");

		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	// Update is called once per frame
	void Update () {
		if (bossHealth <= 0.0) {
			deathTime += Time.deltaTime;
		}

		if (deathTime >= 10)
			Destroy (this.gameObject);

		if(playerHealth.health <= 0)
			bossHealth = 500f;

		if(deathTime >= 5)
			winScreen.SetActive (true);

		SetHealthBar ();
	}

	public void TakeDamage (float amount)
	{
		// Decrement the player's health by amount.
		if (bossHealth - amount < 0)
			bossHealth = 0;
		else
			bossHealth -= amount;
	}

	void SetHealthBar (){
		float barLenth = 1f * bossHealth / 500.0f;
		healthBar.transform.localScale = new Vector3 (barLenth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}