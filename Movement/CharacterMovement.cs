using UnityEngine;

namespace characters
{
    public abstract class CharacterMovement : MonoBehaviour, ICharacterMovement
    {
        #region variables
        public Character character { get; private set; }
        protected bool isInited { get; private set; }

        public virtual bool enableMove { get; set; } = true;
        public virtual bool enableLook { get; set; } = true;

        public float movingSpeed { get; internal set; }
        public float mookingSpeed { get; internal set; }

        /// <summary>
        /// If the character is looking to the direction we return true. False is the opposite.
        /// </summary>
        public bool isLooking  => Vector3.Angle(transform.forward, direction) <= 1;

        internal Vector3 m_LastPosition { get; private set; }
        public Vector3 direction { get; internal set; }
        public Vector3 delta { get; private set; }

        public float magnitude => delta.magnitude;
        #endregion

        #region initialize
        /// <summary>
        /// Initiaze the movement of character.
        /// </summary>
        /// <param name="character"> the main class character. </param>
        public virtual void Initialize(Character character)
        {
            if (character == null) return;

            isInited = true;

            this.character = character;

            movingSpeed = character.characterData.movingSpeed;
            mookingSpeed = character.characterData.lookingSpeed;

            m_LastPosition = transform.position;
        }
        #endregion

        #region controller
        /// <summary>
        /// This function requere to call it in update, fixed update or LateUpdate. It manage the Moving and Looking.
        /// </summary>
        public void UpdateTransfrom(float deltaTime)
        {
            Move(deltaTime);

            Look(deltaTime);
        }

        /// <summary>
        /// To update the delta and last position.
        /// </summary>
        protected void UpdateInfo(float deltaTime)
        {
            delta = (transform.position - m_LastPosition) / deltaTime;

            m_LastPosition = transform.position;
        }
        #endregion

        #region move
        /// <summary>
        /// The function that controller the Move of character.
        /// </summary>
        /// <param name="deltaTime">Time delta between frames.</param>
        public abstract void Move(float deltaTime);

        /// <summary>
        /// Contents the conditions for enable moving.
        /// </summary>
        public virtual bool AllowMove() => true;

        /// <summary>
        /// Change the speed of the movement.
        /// </summary>
        public virtual void SetMovingSpeed(float newSpeed) => movingSpeed = newSpeed;
        #endregion

        #region look
        /// <summary>
        /// Set the look direction. the y axis will be ignored.
        /// </summary>
        public virtual void SetDirection(Vector3 direction) => this.direction = new Vector3(direction.x, 0, direction.z).normalized;

        /// <summary>
        /// Change the speed of the Look to direction.
        /// </summary>
        public virtual void SetLookingSpeed(float newSpeed) => mookingSpeed = newSpeed;

        /// <summary>
        /// Set movement on direction this set will ignore the y.
        /// </summary>
        protected virtual bool AllowLook() => true;

        /// <summary>
        /// The function that controller the look of character.
        /// </summary>
        /// <param name="deltaTime">Time delta between frames.</param>
        public virtual void Look(float deltaTime)
        {
            if (isInited && enableLook && direction != Vector3.zero && !AllowLook())
                return;

            Vector3 lookDelta = Vector3.MoveTowards(transform.forward, direction, mookingSpeed * deltaTime);

            if (lookDelta != Vector3.zero)
                transform.forward = lookDelta;
        }
        #endregion
    }
}