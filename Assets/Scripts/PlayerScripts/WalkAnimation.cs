using UnityEngine;
using StroongerDagger.Movement;


[RequireComponent(typeof(Animator))]

public class WalkAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] Mover PlayerMover;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("bare_hands", true);
    }

    private void Update()
    {
        playerAnimator.SetBool("is_moving", PlayerMover.MoveInput != Vector3.zero);
        
    }




}
