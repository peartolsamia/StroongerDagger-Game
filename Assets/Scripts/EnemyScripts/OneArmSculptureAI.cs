using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OneArmSculptureAI : MonoBehaviour
{
    public bool isAggro = false;

    [Header("Seek")]
    public float maxSpeed = 10f;
    public float arriveThreshold = 0.2f;

    private Transform player;
    private Rigidbody2D rb;
    private int enemyId;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var reporter = GetComponent<EnemyKinematicReporter>();
        enemyId = (reporter != null && !string.IsNullOrEmpty(reporter.CustomId)) ? reporter.CustomId.GetHashCode() : gameObject.GetInstanceID();
    }

    public void StartChasing()
    {
        isAggro = true;
        var go = GameObject.FindWithTag("Player"); 
        if (go != null) player = go.transform;
    }

    void Update()
    {
        if (!isAggro || player == null) return;

        if (EnemyKinematicRegistry.TryGet(enemyId, out var enemyK))
        {
            
            Vector3 dir = (Vector3)player.position - enemyK.Position;

            if (dir.sqrMagnitude < arriveThreshold * arriveThreshold)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            dir.Normalize();
            Vector3 desiredVel = dir * maxSpeed;

            
            enemyK.Velocity = desiredVel;
            enemyK.Position += desiredVel * Time.deltaTime;
            rb.MovePosition(enemyK.Position);

            
            float NewOrientation(float current, Vector3 vel)
            {
                if (vel.sqrMagnitude <= 1e-6f) return current;
                return Mathf.Atan2(-vel.x, vel.y); // radian
            }

            enemyK.Orientation = NewOrientation(enemyK.Orientation, desiredVel);
            rb.MoveRotation(enemyK.Orientation * Mathf.Rad2Deg); 

            
            EnemyKinematicRegistry.Update(enemyId, enemyK);
        }
    }
}
