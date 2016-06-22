using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public TextMesh healthText;
	public float health;
	private float deathTime;
	public GameObject dieScreen;
	GameObject boss;  
	BossHealth bossHealth;

	float limitHealth = 100f;

	bool IsDead = false;

	// Use this for initialization
	void Start () {
		health = limitHealth;
		deathTime = 0f;
		SetHealthText();

		boss = GameObject.FindGameObjectWithTag ("boss");

		bossHealth = boss.GetComponent<BossHealth> ();
	}

	// Update is called once per frame
	void Update () {
		if (health <= 0.0) {
			deathTime += Time.deltaTime;
			dieScreen.SetActive (true);
			IsDead = true;
		}
		else if (health < limitHealth && bossHealth.bossHealth > 0)
			health += Time.deltaTime;
		else if (health > limitHealth)
			health = limitHealth;

		if (deathTime >= 5) {
			limitHealth = 10000f;
			health = limitHealth;
			gameObject.transform.position = new Vector3(650, 1, 720);
			dieScreen.SetActive (false);
			deathTime = 0;
			IsDead = false;
		}

		SetHealthText();
	}

	public void TakeDamage (float amount)
	{
		// Decrement the player's health by amount.
		if (health - amount < 0)
			health = 0;
		else
			health -= amount;
	}

	void SetHealthText()
	{
		healthText.text = ((int)health).ToString ();
	}
}