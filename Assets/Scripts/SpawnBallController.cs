using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnBallController : MonoBehaviour
{
    public Ball ballPrefab;
    public float ballSpeed;
    public float3 direction = new float3(0, 0, 1);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Ball ball = Instantiate(ballPrefab, this.transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().velocity = Vector3.Normalize(direction) * ballSpeed;
            ball.speed = ballSpeed;
        }
    }
}
