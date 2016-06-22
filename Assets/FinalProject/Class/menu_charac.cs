using UnityEngine;
using System.Collections;

public class menu_charac : MonoBehaviour {

	public main c;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void refresh() {
		Transform btn_t = this.GetComponent<Transform>();
		foreach (Transform t in btn_t) {
			if (t.gameObject.name == "NAME") {
				UILabel name = t.gameObject.GetComponent<UILabel> ();
				name.text = "Ethan";
                name.color = Color.white;
			} else if (t.gameObject.name == "LV") {
				UILabel lv = t.gameObject.GetComponentInChildren<UILabel> ();
				lv.text = c.main_char.getLevel ().ToString();
			} else if (t.gameObject.name == "EXP") {
				UILabel exp = t.gameObject.GetComponentInChildren<UILabel> ();
				exp.text = c.main_char.getExp().ToString();
			} else if (t.gameObject.name == "HP") {
				UILabel hp = t.gameObject.GetComponentInChildren<UILabel> ();
				hp.text = c.main_char.getHp().ToString();
			} else if (t.gameObject.name == "ATK") {
				UILabel atk = t.gameObject.GetComponentInChildren<UILabel> ();
				atk.text = c.main_char.getAttack().ToString();
			} else if (t.gameObject.name == "DEF") {
				UILabel def = t.gameObject.GetComponentInChildren<UILabel> ();
				def.text = c.main_char.getDef().ToString();
			} else if (t.gameObject.name == "DEX") {
				UILabel dex = t.gameObject.GetComponentInChildren<UILabel> ();
				dex.text = c.main_char.getDex().ToString();
			} else if (t.gameObject.name == "PT") {
				UILabel pt = t.gameObject.GetComponentInChildren<UILabel> ();
				pt.text = c.main_char.getPt().ToString();
			} 
		}
	}
}
