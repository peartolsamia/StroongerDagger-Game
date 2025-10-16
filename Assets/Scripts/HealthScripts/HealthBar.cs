using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthManager health;        
    [SerializeField] private Image totalHealthbar;           
    [SerializeField] private Image currentHealthbar;         

    private void Start()
    {
        
        totalHealthbar.fillAmount = health.currentHealth / health.MaxHealth; ;
    }

    private void Update()
    {
        if (health == null) return;

        
        float healthRatio = health.currentHealth / health.MaxHealth;

       
        currentHealthbar.fillAmount = healthRatio;
    }
}
