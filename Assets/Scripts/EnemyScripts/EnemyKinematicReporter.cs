using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyKinematicReporter : MonoBehaviour
{
    public string CustomId;
    private float Epsilon = 1e-4f;

    private int _id;
    private Vector3 _prevPos;
    private float _lastOrientation;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _id = string.IsNullOrEmpty(CustomId) ? gameObject.GetInstanceID() : CustomId.GetHashCode();
        _prevPos = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        
        Vector2 delta = new Vector2(pos.x - _prevPos.x, pos.y - _prevPos.y);
        if (delta.sqrMagnitude > Epsilon)
            _lastOrientation = Mathf.Atan2(-delta.x, delta.y); 

        
        Vector3 vel = _rb ? (Vector3)_rb.linearVelocity : (Vector3)delta / Time.deltaTime;
        float rotVel = 0f; 
        if (_rb) rotVel = _rb.angularVelocity * Mathf.Deg2Rad;

        EnemyKinematicRegistry.Update(_id, new EnemyKinematic
        {
            Position = pos,
            Orientation = _lastOrientation,
            Velocity = vel,
            Rotation = rotVel
        });

        _prevPos = pos;
    }

    private void OnDisable()
    {
        EnemyKinematicRegistry.Remove(_id);
    }
}
