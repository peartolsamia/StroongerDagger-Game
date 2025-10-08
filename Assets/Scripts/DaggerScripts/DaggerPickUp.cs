using UnityEngine;

public class DaggerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collider triggered by Player
        if (collision.CompareTag("Player"))
        {
            Animator playerAnimator = collision.GetComponent<Animator>();

            // Set holding_dagger to 1
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("holding_dagger", true);
                playerAnimator.SetBool("bare_hands", false);
            }

            Destroy(gameObject);
        }
    }
}
