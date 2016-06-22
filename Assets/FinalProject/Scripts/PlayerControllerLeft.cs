 using UnityEngine;
using System.Collections;

public class PlayerControllerLeft : MonoBehaviour {

	//public float speed;

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;

	public GameObject camera;

	//private Rigidbody rb;

	void Start () {
		//rb = GetComponent<Rigidbody> ();
		Physics.IgnoreLayerCollision(8,9);
		device = SteamVR_Controller.Input ((int)trackedObj.index);
	}

	// Use this for initialization
	void Update ()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector2 vrPad = device.GetAxis();
		transform.Translate (Vector3.right * vrPad.x * Time.deltaTime * -10 + Vector3.forward * vrPad.y * Time.deltaTime * -10);
		transform.Translate (Vector3.right * h * Time.deltaTime * -10 + Vector3.forward * v * Time.deltaTime * -10);


		if (device.GetTouch(SteamVR_Controller.ButtonMask.Grip)) {
			transform.Rotate (Vector3.up, -100 * Time.deltaTime);
		}

		if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			transform.Translate(0, 5 * Time.deltaTime, 0);
		}

		if (Input.GetKey (KeyCode.F1))
			transform.position = new Vector3 (500, 1, 40);

		if (Input.GetKey (KeyCode.F2))
			transform.position = new Vector3 (150, 1, 250);

		if (Input.GetKey (KeyCode.F3))
			transform.position = new Vector3 (530, 1, -260);

		if (Input.GetKey (KeyCode.F4))
			transform.position = new Vector3 (110, 1, -250);
	}
}