using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D racketRigidbody;
    public float speed = 1.0f;
    public GameObject ballPrefab;

    void Start()
    {
        racketRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float xSpeed = xInput*speed;

        Vector2 newVelocity = new Vector2(xSpeed, 0f);
        racketRigidbody.velocity = newVelocity;
    }
    void Shoot()
    {
        Vector2 dir = new Vector2(-1f, 0f);
        while (Input.GetKey(KeyCode.S))
        {
            dir.x = dir.x + 0.1f;
            if(Input.GetKey(KeyCode.Space))
            {
                Instantiate(ballPrefab);
            }
        }
    }
}
