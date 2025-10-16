using UnityEngine;
using UnityEngine.InputSystem;

namespace StroongerDagger.Movement
{
    public class PlayerRotation : Rotator
    {
        private InputAction lookAction;

        private void OnEnable()
        {
            var playerInput = GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                lookAction = playerInput.actions["Look"];
                lookAction.performed += OnLookPerformed;
            }
        }

        private void OnDisable()
        {
            if (lookAction != null)
                lookAction.performed -= OnLookPerformed;
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            Vector2 LookInput = context.ReadValue<Vector2>();

            if (LookInput.sqrMagnitude > 0.1f)
            {
                Vector3 Target = transform.position + new Vector3(LookInput.x, LookInput.y, 0f);
                LookAt(Target);
            }
        }
    }
}
