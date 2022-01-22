using UnityEngine;

namespace characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class DestinationPhysics : MovementDestination
    {
        protected Rigidbody rigid { get; private set; }
        protected Vector3 velocity { get; private set; }

        /// <summary>
        /// Initiaze the movement of character.
        /// </summary>
        /// <param name="character"> the main class character. </param>
        public override void Initialize(Character character)
        {
            base.Initialize(character);
            rigid = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// This function is execute everytime when the AllowMove return true. You can handle the movement of character from here.
        /// </summary>
        public override void MoveHandle(float deltaTime)
        {
            velocity = Vector3.ClampMagnitude(destination - transform.position, 1);
            rigid.velocity = new Vector3(velocity.x * movingSpeed, rigid.velocity.y, velocity.z * movingSpeed);
        }
    }
}