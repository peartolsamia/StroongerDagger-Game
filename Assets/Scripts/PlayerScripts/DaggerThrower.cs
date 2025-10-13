using UnityEngine;
using StroongerDagger.Combat;

public class DaggerThrower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject daggerPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 12f;

    private DaggerManager daggerManager;
    private bool daggerThrown = false;

    private void Awake()
    {
        daggerManager = GetComponent<DaggerManager>();
    }

    public void TryThrowDagger()
    {
        Debug.Log("Animation event: TryThrowDagger() called!");


        if (daggerThrown) return;
        if (daggerManager.GetCurrentForm() != DaggerFormType.Dagger) return;

        
        GameObject dagger = Instantiate(daggerPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = dagger.GetComponent<Rigidbody2D>();
        rb.linearVelocity = throwPoint.up * throwForce;

        
        daggerManager.SetDaggerForm(DaggerFormType.NoDagger);
        daggerThrown = true;

        
        dagger.GetComponent<ThrownDagger>().Init(this);
    }

    public void OnDaggerReturned()
    {
        daggerThrown = false;
        daggerManager.SetDaggerForm(DaggerFormType.Dagger);
    }
}
