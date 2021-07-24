using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Voxel : MonoBehaviour {
	private new Rigidbody rigidbody;

	public int3 position;

	private void Awake() {
		rigidbody = GetComponent<Rigidbody>();
	}

	public void Fall() {
		rigidbody.isKinematic = false;
	}
}
