using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Ball3 : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D ballRigidbody;

    private bool keepRunning = true;
    private float startTime;

    public float maxtime = 1f;
    public float spawntime = 0.1f;
    void Start()
    {
        startTime = Time.time;
        ballRigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(RepeatEverySeconds());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal") && keepRunning)
        {
            float moveDir = Input.GetAxisRaw("Horizontal"); // -1(¿ÞÂÊ), 1(¿À¸¥ÂÊ)
            transform.position += new Vector3(moveDir * 1f, 0, 0);
        }
        if (!keepRunning)
        {
            ballRigidbody.gravityScale = 1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball 3")
        {
            Die();
        }
        if (collision.gameObject.tag == "Ball 1" || collision.gameObject.tag == "Ball 2" || collision.gameObject.tag == "Ball")
        {
            keepRunning = false;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.score -= 0.5f;
    }

    IEnumerator RepeatEverySeconds()
    {
        while (keepRunning)
        {
            if (Time.time - startTime >= maxtime)
            {
                keepRunning = false;

                break;
            }


            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0f);
            yield return new WaitForSeconds(spawntime);

        }
    }
}
