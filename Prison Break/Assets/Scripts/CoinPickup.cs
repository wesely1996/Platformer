using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<LevelManager>().ScorePoints();
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
