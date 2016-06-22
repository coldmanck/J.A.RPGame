using UnityEngine;
using System.Collections;

public class PlayerControllerRight : MonoBehaviour {

	//public float speed;

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;

	//private Rigidbody rb;

	void Start () {
		//rb = GetComponent<Rigidbody> ();
		Physics.IgnoreLayerCollision(8,9);
		device = SteamVR_Controller.Input ((int)trackedObj.index);
	}

	// Use this for initialization
	void Update ()
	{
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Grip)) {
			transform.Rotate (Vector3.up, 100 * Time.deltaTime);
		}
	}
}