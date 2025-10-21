using UnityEngine;
using System.Collections.Generic;
using System.Linq;



public struct EnemyKinematic
{
    public Vector3 Position;
    public float Orientation;
    public Vector3 Velocity;  // linear velocity
    public float Rotation;  // rotation velocity
}

public struct SteeringOutput
{
    public Vector3 linear; // linear acceleratiion
    public float angular; // angular acceleratiion
}

public static class EnemyKinematicRegistry
{
    private static readonly Dictionary<int, EnemyKinematic> _map = new();

    public static void Update(int id, EnemyKinematic s) => _map[id] = s;
    public static bool TryGet(int id, out EnemyKinematic s) => _map.TryGetValue(id, out s);
    public static void Remove(int id) => _map.Remove(id);



    // To read all
    public static IEnumerable<KeyValuePair<int, EnemyKinematic>> All => _map;
    public static int Count => _map.Count;
}

public static class SteeringIntegrator
{
    
    public static EnemyKinematic Integrate( EnemyKinematic k, SteeringOutput steering, float dt, float maxSpeed = 0f, float maxAngular = 0f)
    {
        
        k.Position = k.Position + k.Velocity * dt;
        k.Orientation = k.Orientation + k.Rotation * dt;

        
        k.Velocity = k.Velocity + steering.linear * dt;
        k.Rotation = k.Rotation + steering.angular * dt;

        
        if (maxSpeed > 0f)
        {
            float sq = k.Velocity.sqrMagnitude;
            float msq = maxSpeed * maxSpeed;
            if (sq > msq) k.Velocity = k.Velocity.normalized * maxSpeed;
        }

        if (maxAngular > 0f)
        {
            if (Mathf.Abs(k.Rotation) > maxAngular)
                k.Rotation = Mathf.Sign(k.Rotation) * maxAngular;
        }

      
        k.Orientation = Mathf.Atan2(Mathf.Sin(k.Orientation), Mathf.Cos(k.Orientation));

        return k;
    }
}

