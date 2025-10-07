using UnityEngine;




namespace StroongerDagger.Movement
{

    [RequireComponent (typeof (Rigidbody2D))] // Rigidbody component is essential for objects that includes Mover component

    public class Mover : MonoBehaviour
    {
        public Vector3 MoveInput; // Current input
        [SerializeField] private float Speed; // Movement speed

        [SerializeField] private float dodgeSpeed; 
        [SerializeField] private float dodgeDuration;
        [SerializeField] private float dodgeCooldown;

        private bool isDodging;
        private Vector2 dodgeDirection;
        private float dodgeTimer;
        private float cooldownTimer;



        private Rigidbody2D Body;



        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
        }




        private void FixedUpdate()
        {
            if (cooldownTimer > 0f)
            { 
                cooldownTimer -= Time.fixedDeltaTime; 
            }

            if (isDodging)
            {
                dodgeTimer -= Time.fixedDeltaTime;
                
                Body.AddForce(dodgeDirection * dodgeSpeed, ForceMode2D.Impulse); // Dodge


                if (dodgeTimer <= 0f)
                {
                    EndDodge();
                }
                    
                return; // Normal moving input is inactive while dodging
            }



            Vector2 move = new Vector2(MoveInput.x, MoveInput.y);

            if (move.sqrMagnitude > 1f) 
            { 
                move.Normalize(); 
            }

            Body.linearVelocity = move * Speed;
        }


        public void StartDodge(Vector3 direction)
        {
            if (cooldownTimer > 0f) return;
            { 
               cooldownTimer = dodgeCooldown; 
            }

            if (isDodging) 
                return; // If its currently dodging, do not start dodge again

            if (direction == Vector3.zero)
            {
                direction = transform.up; // In case there are no move input, dodge to forward
            }
                
            isDodging = true;
            dodgeDirection = new Vector2(direction.x, direction.y).normalized;
            dodgeTimer = dodgeDuration;
        }

        private void EndDodge()
        {
            isDodging = false;
            Body.linearVelocity = Vector2.zero; // Stop dodge
        }


    }





}


