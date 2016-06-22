using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {

	public GameObject menu_root;
	bool menu_status = false;
	public Character main_char;

	public GameObject charac;
	//	public GameObject skill;
	//	public GameObject equip;
	//	public GameObject item;
	//	public GameObject task;
	//	public GameObject system;

	void Awake () {
		main_char = new Character (1, 0, 8, 10, 5, 100, 0);
	}

	// Use this for initialization
	void Start () {
		menu_root.SetActive (menu_status);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			menu_status = !menu_status;
			menu_root.SetActive (menu_status);
			menu_charac mc = charac.gameObject.GetComponent<menu_charac> ();
			mc.refresh ();
		}
	}
}
