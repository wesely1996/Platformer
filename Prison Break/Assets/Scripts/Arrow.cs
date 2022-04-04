using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 5f;

    Rigidbody2D rb2d;
    float direction = 1;

    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        direction = Mathf.Sign(player.localScale.x);
        transform.localScale = new Vector3(direction, 1f, 1f);
        rb2d.velocity = new Vector2(direction * arrowSpeed, 0f);
    }

    void BrakeArrow()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        Invoke("BrakeArrow", 3);
        rb2d.velocity = new Vector2(0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("BrakeArrow", 3);
        rb2d.velocity = new Vector2(0f, 0f);
    }
}
