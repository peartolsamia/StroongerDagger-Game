using UnityEngine;




namespace StroongerDagger.Movement
{

    [RequireComponent (typeof (Rigidbody))] // Rigidbody component is essential for objects that includes Mover component

    public class Mover : MonoBehaviour
    {
        public Vector3 MoveInput; // Current input
        [SerializeField] private float Speed; // Movement speed

        private Rigidbody2D Body;



        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
        }




        private void FixedUpdate()
        {
            Body.linearVelocity = Speed * MoveInput * Time.fixedDeltaTime; // Objects velocity calculation
        }


    }





}


