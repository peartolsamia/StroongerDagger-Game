using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ThrownDagger : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasHit = false;
    private DaggerThrower thrower;

    [SerializeField] private float stopAfterCollisionDelay = 0.05f;
    [SerializeField] private GameObject pickupPrefab;

    public void Init(DaggerThrower daggerThrower)
    {
        thrower = daggerThrower;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit) return;
        hasHit = true;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.isKinematic = true;

        Invoke(nameof(SpawnPickup), stopAfterCollisionDelay);
    }

    private void SpawnPickup()
    {
        if (pickupPrefab != null)
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
