using UnityEngine;
using UnityEngine.InputSystem;



namespace TopDown.Movement
{

    [RequireComponent (typeof (PlayerInput))]

    public class PlayerMovement : Mover
    {

        private void OnMove(InputValue Value)
        {
            Vector3 PlayerInput = new Vector3(Value.Get<Vector2>().x, Value.Get<Vector2>().y, 0);
            Input = PlayerInput;
        }


    }




}
