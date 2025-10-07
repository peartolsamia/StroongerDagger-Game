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
    }

    private void Update()
    {
        CurrentWalkAnimation.SetBool("is_moving", PlayerMover.MoveInput != Vector3.zero);
        CurrentWalkAnimation.SetBool("bare_hands", true);
    }




}
