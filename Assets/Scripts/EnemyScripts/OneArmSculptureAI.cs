using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OneArmSculptureAI : MonoBehaviour
{
    private Animator oneArmAnimator;

    public bool isAggro = false;

    [Header("Seek")]
    public float maxSpeed = 10f;
    public float radius = 2.5f; // Arrive satisfaction radius
    public float timeToTarget = 0.25f;

    private Transform player;
    private Rigidbody2D rb;
    private int enemyId;

    private float timer = 0f;
    private bool awakeFirstTime = true;

    void Awake()
    {
        oneArmAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        var reporter = GetComponent<EnemyKinematicReporter>();
        enemyId = (reporter != null && !string.IsNullOrEmpty(reporter.CustomId)) ? reporter.CustomId.GetHashCode() : gameObject.GetInstanceID();
    }

    public void StartChasing()
    {
        oneArmAnimator.SetBool("awake", true);

        isAggro = true;
        var go = GameObject.FindWithTag("Player"); 
        if (go != null) player = go.transform;
    }

    void Update()
    {
        if (!isAggro || player == null) return;

        if (awakeFirstTime)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                awakeFirstTime = false;

            }
        }

        if (EnemyKinematicRegistry.TryGet(enemyId, out var enemyK) && awakeFirstTime == false)
        {
            Vector3 dir = (Vector3)player.position - enemyK.Position;

            if (dir.sqrMagnitude < radius * radius)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            float distance = dir.magnitude;                // NEW
            float desiredSpeed = distance / timeToTarget;  // NEW

            // NEW: Çok uzaksa bu hýz maxSpeed'i aþabilir; clip et
            if (desiredSpeed > maxSpeed)
                desiredSpeed = maxSpeed;

            // NEW: Yön * (mesafeye göre ayarlanmýþ hýz)
            Vector3 desiredVel = (distance > 1e-4f) ? (dir / distance) * desiredSpeed : Vector3.zero;
            // --- /ARRIVE HIZ HESABI ---


            enemyK.Velocity = desiredVel;
            enemyK.Position += desiredVel * Time.deltaTime;
            rb.MovePosition(enemyK.Position);

            
            float NewOrientation(float current, Vector3 vel)
            {
                if (vel.sqrMagnitude <= 1e-4f) return current;
                return Mathf.Atan2(-vel.x, vel.y); // radian
            }

            enemyK.Orientation = NewOrientation(enemyK.Orientation, desiredVel);
            rb.MoveRotation(enemyK.Orientation * Mathf.Rad2Deg); 

            
            EnemyKinematicRegistry.Update(enemyId, enemyK);
        }
    }
}
