using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    // When the player enters the coin trigger, increase score and destroy the coin
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Increase the score by 1
            ScoreManager.Instance.AddScore(1);
            // Destroy this coin object to simulate collection
            Destroy(gameObject);
        }
    }
}
