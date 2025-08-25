using UnityEngine;




namespace TopDown.Movement
{

    [RequireComponent (typeof (Rigidbody))]

    public class Mover : MonoBehaviour
    {
        protected Vector3 Input; // Current Input
        [SerializeField] private float Speed; // Movement Speed

        private Rigidbody2D Body;



        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
        }




        private void FixedUpdate()
        {
            Body.linearVelocity = Speed * Input * Time.fixedDeltaTime;
        }


    }





}


