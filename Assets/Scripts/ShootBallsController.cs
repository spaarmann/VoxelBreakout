using UnityEngine;

public class ShootBallsController : MonoBehaviour {
	public Rigidbody ballPrefab;
	public float ballSpeed;

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Rigidbody ball = Instantiate(ballPrefab, ray.origin, Quaternion.identity);
			ball.velocity = ray.direction * ballSpeed;
		}
	}
}