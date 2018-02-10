using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float MaxSpeed {
		get {
			return 39f;
		}
	}

	Rigidbody body;

	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	void Update () {
		Vector3 vel = body.velocity;
		if (Input.GetKey (KeyCode.W))
			vel += 3f * transform.forward;
		if (Input.GetKey (KeyCode.S))
			vel -= 3f * transform.forward;
		if (Input.GetKey (KeyCode.A))
			vel -= 3f * transform.right;
		if (Input.GetKey (KeyCode.D))
			vel += 3f * transform.right;
		float y_component = vel.y;
		if (vel.magnitude > MaxSpeed) {
			vel.Normalize ();
			vel.Scale (Vector3.one * MaxSpeed);
		}
		vel.x *= 0.96f;
		vel.z *= 0.96f;
		vel.y = y_component;
		if (Input.GetKeyDown (KeyCode.Space) && Physics.Raycast (transform.position, Vector3.down, 6f)) {
			body.AddRelativeForce (Vector3.up * 1000, ForceMode.Impulse);
		}
		body.AddRelativeForce (Vector3.down * 250, ForceMode.Force);

		body.velocity = vel.magnitude > 0.5f ? vel : Vector3.zero;
	}
}
