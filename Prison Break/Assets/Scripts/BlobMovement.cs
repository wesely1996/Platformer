using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Rigidbody2D rb2d;
    float direction = 1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(direction * movementSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        direction *= -1f;
        transform.localScale = new Vector3(direction, 1f, 1f);
    }

}
