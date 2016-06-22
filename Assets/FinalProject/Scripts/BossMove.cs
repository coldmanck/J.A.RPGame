using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour
{
	GameObject player;               // Reference to the player's position.
	Rigidbody m_position;
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	Animation anim; 

	BossHealth mHealth;
	PlayerHealth playerHealth;

	Vector3 OriPosition;
	bool goHome;
	public bool isAttack = false;

	int mAnimation;
	bool isdead = false;
	float deadTime = 0.0f;

	float distanceWithTarget;

	void Awake ()
	{
		m_position = GetComponent <Rigidbody> ();
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent<Animation> ();
		mHealth = GetComponent<BossHealth> ();

		player = GameObject.FindGameObjectWithTag ("Player");

		playerHealth = player.GetComponent<PlayerHealth> ();

		OriPosition = m_position.position;

		goHome = true;
	}


	void Update ()
	{
		changeAnimation ();
	}

	void changeAnimation()
	{
		if (mHealth.bossHealth > 0) {
			distanceWithTarget = Vector3.Distance (player.transform.position, m_position.position);

			if (((distanceWithTarget < 30 && playerHealth.health > 0) || mHealth.bossHealth < 500) && player.transform.position.y >= 50) {
				transform.LookAt (player.transform.position);
				isAttack = true;

				if (distanceWithTarget > 4) {
					anim.Play ("Run");
					nav.speed = 10;
					nav.SetDestination (player.transform.position);
				} else {
					anim.Play ("Attack_01");
					nav.SetDestination (m_position.position);
				}

				goHome = true;
			} else {
				isAttack = false;
				GoHome();
			}
		} else {
			if (!isdead) {
				anim.Play ("Die");
				deadTime += Time.deltaTime;

				if (deadTime >= 1.5)
					isdead = true;
			} else
				anim.Stop ();
			
			//nav.SetDestination (m_position.position);
			nav.Stop ();
			isAttack = false;
		}
	}

	void GoHome ()
	{
		if (goHome) {
			anim.Play ("Walk");
			nav.speed = 3;
			nav.SetDestination (OriPosition);
		}

		if (Vector3.Distance (OriPosition, m_position.position) < 2) {
			goHome = false;
			anim.Play ("Idle");
		}
	}
}