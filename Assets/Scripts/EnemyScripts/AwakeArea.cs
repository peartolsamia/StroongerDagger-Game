using UnityEngine;

public class AwakeArea : MonoBehaviour
{
    private OneArmSculptureAI enemyAI;

    private void Awake()
    {
        enemyAI = GetComponentInParent<OneArmSculptureAI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAI.StartChasing();
        }
    }
}