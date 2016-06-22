using UnityEngine;
using System.Collections;

public class SkeletonMovementScript : MonoBehaviour
{
	public GameObject point1;
	public GameObject point2;
	public GameObject point3;

	GameObject player;               // Reference to the player's position.
	Rigidbody m_position;
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	Animator anim; 

	MonsterHealth mHealth;
	Vector3 OriPosition;
	PlayerHealth playerHealth;

	bool goHome;

	int mAnimation;
	/*
	Idle:0
	Run:1
	Attack:2
	Death:3
	Walk:4
	*/

	float distanceWithTarget;

	void Awake ()
	{
		m_position = GetComponent <Rigidbody> ();
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
		mHealth = GetComponent<MonsterHealth> ();

		player = GameObject.FindGameObjectWithTag ("Player");

		playerHealth = player.GetComponent<PlayerHealth> ();

		OriPosition = m_position.position;

		mAnimation = 0;
		goHome = true;
	}


	void Update ()
	{
		changeAnimation ();

		anim.SetInteger ("mAnimation" , mAnimation);
	}

	void changeAnimation()
	{
		if (mHealth.monsterHealth > 0) {
			distanceWithTarget = Vector3.Distance (player.transform.position, m_position.position);

			if (((distanceWithTarget < 30 && playerHealth.health > 0) || mHealth.monsterHealth < 100) && player.transform.position.y < 2) {
				transform.LookAt(player.transform.position);

				if (distanceWithTarget > 4) {
					mAnimation = 1;
					nav.speed = 7;
					nav.SetDestination (player.transform.position);
				} else {
					mAnimation = 2;
					nav.SetDestination (m_position.position);
				}

				goHome = true;
			} else {
				//mAnimation = 0;
				//nav.SetDestination (m_position.position);
				GoHome();
				AutoMoving ();
			}
		} else {
			mAnimation = 3;
			nav.SetDestination (m_position.position);
		}
	}

	void GoHome ()
	{
		if (goHome) {
			mAnimation = 4;
			nav.speed = 3;
			nav.SetDestination (point1.transform.position);
		}

		goHome = false;
	}

	void AutoMoving ()
	{
		if (Vector3.Distance (point1.transform.position, m_position.position) < 3) {
			mAnimation = 4;
			nav.speed = 2;
			nav.SetDestination (point2.transform.position);
		}else if (Vector3.Distance (point2.transform.position, m_position.position) < 3){
			mAnimation = 4;
			nav.speed = 2;
			nav.SetDestination (point3.transform.position);
		}else if (Vector3.Distance (point3.transform.position, m_position.position) < 3){
			mAnimation = 4;
			nav.speed = 2;
			nav.SetDestination (point1.transform.position);
		}
	}
}