using UnityEngine;
using System.Collections;

public class PC_PlayerController : MonoBehaviour {

	//public float speed;
	private float goBackwardSpeed;
	private float goForwardSpeed;
	private float turnSpeed;
	private float actionTime;

	private Animation anim;
	//private Rigidbody rb;

	bool IsAttack;
	bool IsJump;
	bool IsGoForward;
	bool IsGoBackward;
	bool IsTurnRight;
	bool IsTurnLeft;
	bool IsKeyPress;
	bool IsAction;

	void Start () {
		//rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();

		actionTime = 0;
		goBackwardSpeed = 3;
		goForwardSpeed = 10;
		turnSpeed = 100;

		IsAction = false;
	}

	// Use this for initialization
	void Update ()
	{
		InitialBool ();

		KeypressDetection ();

		Action ();
	}

	void InitialBool()
	{
		if (!IsAction) {
			IsAttack = false;
			IsJump = false;
			IsGoForward = false;
			IsGoBackward = false;
			IsTurnRight = false;
			IsTurnLeft = false;
			IsKeyPress = false;
		}
	}

	void KeypressDetection ()
	{
		if (!IsAction) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				IsGoForward = true;
			}

			if (Input.GetKey (KeyCode.DownArrow)) {
				IsGoBackward = true;
			}

			if (Input.GetKey (KeyCode.LeftArrow)) {
				IsTurnLeft = true;
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				IsTurnRight = true;
			}

			if (Input.GetKey (KeyCode.Space)) {
				IsJump = true;
			}

			if (Input.GetKey (KeyCode.Z)) {
				IsAttack = true;
			}
		}
	}

	void Action()
	{
		if (IsAttack) {
			anim.Play ("Attack");
			IsKeyPress = true;
			IsAction = true;
			Timer (1.5f);
		}

		if(IsJump && !IsAttack){
			transform.Translate(0, 5 * Time.deltaTime, 0);
			anim.Play ("Jump");
			IsKeyPress = true;
			IsAction = true;
			Timer (1);
		}

		if (IsGoForward && !IsAttack && !IsGoBackward && !IsJump) {
			transform.Translate (Vector3.forward * goForwardSpeed * Time.deltaTime);
			IsKeyPress = true;
			anim.Play ("Run");
		}

		if (IsGoBackward && !IsAttack && !IsGoForward && !IsJump) {
			transform.Translate (-Vector3.forward * goBackwardSpeed * Time.deltaTime);
			IsKeyPress = true;
			anim.Play ("Walk");
		}

		if (IsTurnLeft && ! IsJump && !IsTurnRight) {
			transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);
			IsKeyPress = true;

			if (!IsAttack && !IsGoBackward && !IsGoForward)
				anim.Play ("Walk");
		}

		if (IsTurnRight && ! IsJump && !IsTurnLeft) {
			transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
			IsKeyPress = true;

			if( !IsAttack && !IsGoBackward && !IsGoForward )
				anim.Play ("Walk");
		}

		if(!IsKeyPress)
			anim.Play ("idle");
	}

	void Timer (float totalActionTime)
	{
		actionTime += Time.deltaTime;

		if (actionTime >= totalActionTime) {
			IsAction = false;
			actionTime = 0;
		}
	}
}