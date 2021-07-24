using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour {
	public bool keepVelocityAfterCollision = false;

	private new Rigidbody rigidbody;
	private Vector3 lastVelocity;

	private void Awake() {
		rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		lastVelocity = rigidbody.velocity;
	}

	private void OnCollisionEnter(Collision col) {
		if (keepVelocityAfterCollision) {
			rigidbody.AddForce(-col.impulse, ForceMode.Impulse);
			//rigidbody.velocity = lastVelocity;
		}
	}

	private void OnCollisionStay(Collision col) {
		if (keepVelocityAfterCollision) {
			rigidbody.AddForce(-col.impulse, ForceMode.Impulse);
		}
	}
}