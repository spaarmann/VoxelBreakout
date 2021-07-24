using UnityEngine;

public class ShootBallsController : MonoBehaviour {
	public Ball ballPrefab;
	public float ballSpeed;

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Ball ball = Instantiate(ballPrefab, ray.origin, Quaternion.identity);
			ball.GetComponent<Rigidbody>().velocity = ray.direction * ballSpeed;
			ball.speed = ballSpeed;
		}
	}
}