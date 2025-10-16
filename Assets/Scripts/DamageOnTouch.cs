using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            
            HealthManager health = collision.GetComponent<HealthManager>();

            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
