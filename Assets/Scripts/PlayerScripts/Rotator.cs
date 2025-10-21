using UnityEngine;
using UnityEngine.Playables;



namespace StroongerDagger.Movement
{

    public class Rotator : MonoBehaviour
    {

        protected void LookAt(Vector3 Target)
        {
            float Angle = AngleBetweenTwoPoints(transform.position, Target) + 90;

            transform.eulerAngles = new Vector3 (0, 0, Angle);

        }


        private float AngleBetweenTwoPoints(Vector3 p1, Vector3 p2)
        {
            return Mathf.Atan2(p1.y - p2.y, p1.x - p2.x) * Mathf.Rad2Deg;
        }

    }


}
