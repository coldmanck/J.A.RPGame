using UnityEngine;
using System.Collections;

public class Dialogue3 : MonoBehaviour {

	GameObject player;               // Reference to the player's position.
	//Rigidbody m_position;
	float distanceWithTarget;
	float timer;
	bool inConversation;

	public GameObject title;

	// Use this for initialization
	void Start () {
		timer = 0;
		//m_position = GetComponent <Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		distanceWithTarget = Vector3.Distance (player.transform.position, gameObject.transform.position);

		if (distanceWithTarget < 8)
			DialogueStart ();
		else
			Reset ();
	}

	void DialogueStart(){
		int unitTime = 0, constant = 3;

		if (timer < constant * ++unitTime)
			title.SetActive (true);
		else {
			title.SetActive (false);
			Reset ();
		}

		timer += Time.deltaTime;
	}

	void setText(GameObject text1, GameObject text2){
		text1.SetActive (false);
		text2.SetActive (true);
	}

	void Reset(){
		title.SetActive (false);
		timer = 0;
	}
}
