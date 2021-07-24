using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Voxel : MonoBehaviour {
	private new Rigidbody rigidbody;

	private void Awake() {
		rigidbody = GetComponent<Rigidbody>();
	}

	public void Fall() {
		rigidbody.isKinematic = false;
	}
}
