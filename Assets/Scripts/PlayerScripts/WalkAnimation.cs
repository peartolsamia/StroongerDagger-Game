using UnityEngine;
using StroongerDagger.Movement;


[RequireComponent(typeof(Animator))]

public class WalkAnimation : MonoBehaviour
{
    private Animator CurrentWalkAnimation;
    [SerializeField] Mover PlayerMover;


    private void Awake()
    {
        CurrentWalkAnimation = GetComponent<Animator>();
        CurrentWalkAnimation.SetBool("bare_hands", true);
    }

    private void Update()
    {
        CurrentWalkAnimation.SetBool("is_moving", PlayerMover.MoveInput != Vector3.zero);
        
    }




}
