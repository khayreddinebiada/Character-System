using UnityEngine;

namespace characters
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class DirectionPhysics : CharacterMovement
    {
        #region variables
        protected Rigidbody m_Rigidbody;
        #endregion

        #region initialize
        /// <summary>
        /// Initiaze the movement of character.
        /// </summary>
        /// <param name="character"> the main class character. </param>
        public override void Initialize(Character character)
        {
            base.Initialize(character);

            m_Rigidbody = GetComponent<Rigidbody>();

            if (m_Rigidbody.freezeRotation == false)
                Debug.LogError("Please freeze all rotation of rigidbody.");
        }
        #endregion

        #region transform
        /// <summary>
        /// The function that controller the Move of character.
        /// </summary>
        /// <param name="deltaTime">Time delta between frames.</param>
        public override void Move(float deltaTime)
        {
            if (isInited && enableMove && AllowMove())
            {
                SetVelocity(direction * movingSpeed);
            }
            else
            {
                SetVelocity(Vector3.MoveTowards(m_Rigidbody.velocity, Vector3.zero, movingSpeed * deltaTime));
            }
        }

        private void SetVelocity(Vector3 newVelocity)
        {
            m_Rigidbody.velocity = new Vector3(newVelocity.x, m_Rigidbody.velocity.y, newVelocity.z);
        }
        #endregion
    }
}
