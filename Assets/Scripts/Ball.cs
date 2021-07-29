using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Ball : MonoBehaviour {
	public bool keepVelocityAfterCollision = false;
	public LayerMask voxelLayer;
	public int maxCollisions = 3;
	public float speed;
	public static float timeout = 0.5F;
	

	private new Rigidbody rigidbody;
	private SphereCollider sphereCollider;
	private int numberOfCollisions;

	private float lastCollisionTime;


	private void Awake() {
		rigidbody = GetComponent<Rigidbody>();
		sphereCollider = GetComponent<SphereCollider>();
	}

	private void FixedUpdate() {
		rigidbody.velocity = rigidbody.velocity.normalized * speed;
	}

	private void Update() {
		if (Time.time > lastCollisionTime + timeout)
			numberOfCollisions = 0;
	}

	private void OnTriggerEnter(Collider other) {
		lastCollisionTime = Time.time;
		if (numberOfCollisions < maxCollisions) {
			numberOfCollisions++;
			return;
		}

		numberOfCollisions = 0;
		RaycastHit[] hits = Physics.SphereCastAll(rigidbody.position - rigidbody.velocity * 0.1f, sphereCollider.radius, rigidbody.velocity.normalized, rigidbody.velocity.magnitude * 1.5f, ~voxelLayer);
		RaycastHit desiredHit = hits.FirstOrDefault(elem => elem.collider == other);
		if (desiredHit.collider != null) {
			rigidbody.velocity = GetUpdatedVelocity(sphereCollider.radius, desiredHit);
		} else {
			Debug.Log($"Origin Point of SphereCast: {rigidbody.position - rigidbody.velocity * 1f}");
			Debug.Log($"Direction: {rigidbody.velocity.normalized}");
			Debug.Log($"Max Distance: {rigidbody.velocity.magnitude * 10}");
			Debug.Log($"Erwartet: {other.name}");
			Debug.Log(string.Join(", ", hits.Select(h => h.collider.name)));
			throw new OhNoException("Oh no!");
		}
	}

	private Vector3 GetUpdatedVelocity(float sphereRadius, RaycastHit hitInfo) {
		Vector3 updatedVelocity = Vector3.Reflect(this.rigidbody.velocity.normalized, hitInfo.normal) * this.speed;
		
		// collide with moving object
		if (hitInfo.rigidbody != null) {
			updatedVelocity = (updatedVelocity.normalized + hitInfo.rigidbody.velocity.normalized).normalized * this.speed;
		}

		return updatedVelocity;
	}
}