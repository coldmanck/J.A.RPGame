using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	public AudioSource audio1, audio2;

	// Use this for initialization
	void Start () {
		audio1 = GetComponent<AudioSource> ();
		//audio2 = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
