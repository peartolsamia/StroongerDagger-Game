using UnityEngine;
using UnityEngine.InputSystem;



namespace StroongerDagger.Movement
{

    [RequireComponent (typeof (PlayerInput))] // PlayerInput component is essential for objects that includes PlayerMovement component

    public class PlayerMovement : Mover
    {

        private void OnMove(InputValue Value) // Takes move inputs and sets Mover class Input value with it
        {
            Vector3 PlayerInput = new Vector3(Value.Get<Vector2>().x, Value.Get<Vector2>().y, 0);
            MoveInput = PlayerInput;
        }


    }




}
