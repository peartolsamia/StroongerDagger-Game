using UnityEngine;
using StroongerDagger.Combat;

public class DaggerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var daggerManager = collision.GetComponent<DaggerManager>();
            var thrower = collision.GetComponent<DaggerThrower>();

            if (daggerManager != null)
            {
                daggerManager.SetDaggerForm(DaggerFormType.Dagger);
            }

            if (thrower != null)
            {
                thrower.OnDaggerReturned();
            }

            Destroy(gameObject);
        }
    }
}
