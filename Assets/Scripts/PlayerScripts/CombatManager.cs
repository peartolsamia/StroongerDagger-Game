using UnityEngine;
using UnityEngine.InputSystem;
using StroongerDagger.Combat;

[RequireComponent(typeof(Animator))]
public class CombatManager : MonoBehaviour
{
    private Animator playerAnimator;
    private DaggerManager daggerManager;
    private DaggerThrower daggerThrower;

    [Header("Input Actions")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction attack1Action;
    private InputAction attack2Action;
    private InputAction increaseFormAction;
    private InputAction decreaseFormAction;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        daggerManager = GetComponent<DaggerManager>();
        daggerThrower = GetComponent<DaggerThrower>();
    }

    private void OnEnable()
    {
        var actionMap = inputActions.FindActionMap("Player");
        attack1Action = actionMap.FindAction("Attack1");
        attack2Action = actionMap.FindAction("Attack2");
        increaseFormAction = actionMap.FindAction("IncreaseDagger");
        decreaseFormAction = actionMap.FindAction("DecreaseDagger");

        attack1Action.performed += _ => Attack1();
        attack2Action.performed += _ => Attack2();
        increaseFormAction.performed += _ => daggerManager.IncreaseForm();
        decreaseFormAction.performed += _ => daggerManager.DecreaseForm();

        attack1Action.Enable();
        attack2Action.Enable();
        increaseFormAction.Enable();
        decreaseFormAction.Enable();
    }

    private void OnDisable()
    {
        attack1Action.Disable();
        attack2Action.Disable();
        increaseFormAction.Disable();
        decreaseFormAction.Disable();
    }

    private void Attack1() // Standard Attack
    {
        if (daggerManager.GetCurrentForm() == DaggerFormType.NoDagger) // Do nothing if current form is no dagger
            return;

        playerAnimator.SetTrigger("Attack1");
        Debug.Log("Attack1 performed");
    }

    private void Attack2() // Special Attack
    {
        if (daggerManager.GetCurrentForm() == DaggerFormType.NoDagger) // Do nothing if current form is no dagger
            return;

        if (daggerManager.GetCurrentForm() == DaggerFormType.Dagger) // Dagger throw 
        {
            playerAnimator.SetTrigger("Attack2");
            //Debug.Log("Current clip: " + playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            //Debug.Log("Attack2 trigger sent! holding_dagger = " + playerAnimator.GetBool("holding_dagger"));
        }
        else
        {
            Debug.Log("No dagger to throw!");
        }

        if (daggerManager.GetCurrentForm() == DaggerFormType.Sword || daggerManager.GetCurrentForm() == DaggerFormType.Greatsword) // TEMPORARILY for other cases (greatsword and sword) 
        {
            playerAnimator.SetTrigger("Attack2");
        }
    }
}
