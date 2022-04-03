using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Rigidbody2D rigidbody;
    BoxCollider2D wallDetector;
    float direction = 1f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        wallDetector = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(direction * movementSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        direction *= -1f;
        transform.localScale = new Vector3(direction, 1f, 1f);
    }
}
