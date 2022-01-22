using UnityEngine;
using UnityEngine.AI;

namespace characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class DestinationNavMesh : MovementDestination
    {
        protected NavMeshAgent agent { get; private set; }

        /// <summary>
        /// Initiaze the movement of character.
        /// </summary>
        /// <param name="character"> the main class character. </param>
        public override void Initialize(Character character)
        {
            base.Initialize(character);
            agent = GetComponent<NavMeshAgent>();
            agent.speed = movingSpeed;
        }

        /// <summary>
        /// This function is execute everytime when the AllowMove return true. You can handle the movement of character from here.
        /// </summary>
        public override void MoveHandle(float deltaTime) { }

        /// <summary>
        /// Set the destination where you want go. Next Position.
        /// </summary>
        public override void SetDestination(Vector3 destination)
        {
            base.SetDestination(destination);
            agent.SetDestination(destination);
        }

        /// <summary>
        /// Change the speed of the movement.
        /// </summary>
        public override void SetMovingSpeed(float newSpeed)
        {
            base.SetMovingSpeed(newSpeed);
            agent.speed = newSpeed;
        }
    }
}
