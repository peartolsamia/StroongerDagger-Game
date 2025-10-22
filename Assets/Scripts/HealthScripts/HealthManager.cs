using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using StroongerDagger.Movement;


public class HealthManager : MonoBehaviour
{
    [Header("Health")]

    [SerializeField] private float maxHealth = 100f;
    public float MaxHealth => maxHealth;
    [SerializeField] private float startingHealth = 50f;


    public float currentHealth { get; private set; } // public to get method but private to set

    private Animator anim;

    private bool dead;


    [Header("iFrames")]

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;

    private bool isInvulnerable = false;

    private SpriteRenderer spriteRend;

    
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    
 


    public void TakeDamage(float _damage)
    {
        if (isInvulnerable || dead) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); 

        if (currentHealth > 0) // not dead
        {
            StartCoroutine(Invulnerability());
        }
        else //  dead
        {
            if (!dead)
            {
                anim.SetBool("is_alive", false);


                // disable players control

                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }

                if (GetComponent<PlayerRotation>() != null)
                {
                    GetComponent<PlayerRotation>().enabled = false;
                }

                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().SetBool("is_moving", false);
                }

                // disable and destroy enemy
                // ...






                dead = true;
            }
        }
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability() // Enter IFrame
    {
        isInvulnerable = true;

        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numOfFlashes; i++) // flashing red and becomes a little trasperent while invulnerable
        {
            spriteRend.color = new Color(1, 0, 0, 0.8f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);

        isInvulnerable = false;
    }


    public void UpgradeHealth(float newMaxHealth)
    {
        startingHealth = newMaxHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
    }


}
