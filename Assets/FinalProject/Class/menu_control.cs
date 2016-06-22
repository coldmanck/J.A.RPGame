using UnityEngine;
using System.Collections;

public class menu_control : MonoBehaviour {

	public GameObject charac;
//	public GameObject skill;
//	public GameObject equip;
//	public GameObject item;
//	public GameObject task;
//	public GameObject system;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeToCharac() {
		menu_charac mc = charac.gameObject.GetComponent<menu_charac> ();
		mc.refresh ();
	}
}
