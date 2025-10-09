using UnityEngine;
using StroongerDagger.Combat;

[RequireComponent(typeof(Animator))]
public class DaggerManager : MonoBehaviour
{
    private Animator playerAnimator;
    private DaggerFormType currentDaggerForm = DaggerFormType.NoDagger;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        UpdateDaggerFormAnimatorParams();
    }

    public DaggerFormType GetCurrentForm() => currentDaggerForm;

    public void SetDaggerForm(DaggerFormType newForm)
    {
        currentDaggerForm = newForm;
        UpdateDaggerFormAnimatorParams();
    }

    public void IncreaseForm()
    {
        if (currentDaggerForm == DaggerFormType.NoDagger || currentDaggerForm == DaggerFormType.Greatsword)// Do nothing if current form is no dagger or greatsword
            return;

        if (currentDaggerForm == DaggerFormType.Sword) // Sword -> Greatsword
        { 
            currentDaggerForm++; 
            UpdateDaggerFormAnimatorParams(); 
        } 

        if (currentDaggerForm == DaggerFormType.Dagger) // Dagger -> Sword
        { 
            currentDaggerForm++; 
            UpdateDaggerFormAnimatorParams(); 
        }
    }

    public void DecreaseForm()
    {
        if (currentDaggerForm == DaggerFormType.NoDagger || currentDaggerForm == DaggerFormType.Dagger) // Do nothing if current form is no dagger or dagger
            return;

        if (currentDaggerForm == DaggerFormType.Sword) // Sword -> Dagger
        { 
            currentDaggerForm--; 
            UpdateDaggerFormAnimatorParams(); 
        } 
        
        if (currentDaggerForm == DaggerFormType.Greatsword) // Greatsword -> Sword
        { 
            currentDaggerForm--; 
            UpdateDaggerFormAnimatorParams(); 
        }
    }

    private void UpdateDaggerFormAnimatorParams()
    {
        playerAnimator.SetBool("bare_hands", currentDaggerForm == DaggerFormType.NoDagger);
        playerAnimator.SetBool("holding_dagger", currentDaggerForm == DaggerFormType.Dagger);
        playerAnimator.SetBool("holding_sword", currentDaggerForm == DaggerFormType.Sword);
        playerAnimator.SetBool("holding_greatsword", currentDaggerForm == DaggerFormType.Greatsword);
    }
}
