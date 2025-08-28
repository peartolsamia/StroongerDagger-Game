using UnityEngine;
using UnityEngine.InputSystem;



namespace StroongerDagger.Movement
{

    public class PlayerRotation : Rotator
    {

        private void OnLook(InputValue Value)
        {
            Vector2 LookInput = Value.Get<Vector2>();

            if (LookInput.sqrMagnitude > 0.1f) 
            {
                Vector3 Target = transform.position + new Vector3(LookInput.x, LookInput.y, 0f);

                LookAt(Target);
            }
        }


    }

}
