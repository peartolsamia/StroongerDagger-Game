using UnityEngine;
using UnityEngine.InputSystem;



namespace StroongerDagger.Movement
{

    [RequireComponent(typeof(PlayerInput))] // PlayerInput component is essential for objects that includes PlayerMovement component

    public class PlayerMovement : Mover
    {

        Vector3 previousPos;
        Vector3 currentPos;
        float orientation;
        const float Epsilon = 1e-4f;


        private void Start()
        {
            previousPos = transform.position;
        }

        private void LateUpdate()
        {
            currentPos = transform.position;
            UpdateStatic();
            previousPos = currentPos;

            /*
            if (Time.frameCount % 10 == 0)
                Debug.Log($"Pos: {PlayerStatic.Position}, $Ori(deg): {PlayerStatic.Orientation * Mathf.Rad2Deg}");
            */


        }



        private void OnMove(InputValue Value) // Takes move inputs and sets Mover class Input value with it
        {
            Vector3 PlayerInput = new Vector3(Value.Get<Vector2>().x, Value.Get<Vector2>().y, 0);
            MoveInput = PlayerInput;
        }

        private void OnDodge(InputValue value)
        {
            if (value.isPressed) // Only when button pressed
            {
                TryStartDodge();
            }
        }

        private void TryStartDodge()
        {
            // Tell mover class to start dodge
            StartDodge(MoveInput);
        }



        private void UpdateStatic()
        {
            Vector3 delta = currentPos - previousPos;


            if (delta.sqrMagnitude > Epsilon)
            {
                orientation = Mathf.Atan2(-delta.x, delta.y);
            }
                

            PlayerStatic.Position = currentPos;
            PlayerStatic.Orientation = orientation;
        }


    }




}
