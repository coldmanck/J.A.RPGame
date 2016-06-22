using UnityEngine;
using System.Collections;

public class Dialogue2 : MonoBehaviour {

	GameObject player;               // Reference to the player's position.
	//Rigidbody m_position;
	float distanceWithTarget;
	float timer;
	bool inConversation;

	public GameObject title;
	public GameObject dialogue;
	public GameObject dialogue2;
	public GameObject dialogue3;
	public GameObject dialogue4;
	public GameObject dialogue5;
	public GameObject dialogue6;

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
		else if (timer < constant * ++unitTime)
			setText (title, dialogue);
		else if (timer < constant * ++unitTime)
			setText (dialogue, dialogue2);
		else if (timer < constant * ++unitTime)
			setText (dialogue2, dialogue3);
		else if (timer < constant * ++unitTime)
			setText (dialogue3, dialogue4);
		else if (timer < constant * ++unitTime)
			setText (dialogue4, dialogue5);
		else if (timer < constant * ++unitTime)
			setText (dialogue5, dialogue6);
		else {
			dialogue6.SetActive (false);
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
		dialogue.SetActive (false);
		dialogue2.SetActive (false);
		dialogue3.SetActive (false);
		dialogue4.SetActive (false);
		dialogue5.SetActive (false);
		dialogue6.SetActive (false);
		timer = 0;
	}
}
