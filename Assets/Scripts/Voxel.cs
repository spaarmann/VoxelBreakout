using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Voxel : MonoBehaviour {
	public float breakSpeedThreshold = 10f;

	private new Rigidbody rigidbody;

	[field: SerializeField, HideInInspector]
	public int3 position { get; private set; }
	[field: SerializeField, HideInInspector]
	public Level level { get; private set; }

	public void Initialize(Level lvl, int3 pos) {
		position = pos;
		level = lvl;
	}

	private void Awake() {
		rigidbody = GetComponent<Rigidbody>();
	}

	public void Fall() {
		rigidbody.isKinematic = false;
	}

	private void OnCollisionEnter(Collision col) {
		if (col.relativeVelocity.magnitude >= breakSpeedThreshold) {
			level.OnVoxelHit(this);
		}
	}
}
