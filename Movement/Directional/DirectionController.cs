using UnityEngine;

namespace characters
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class DirectionController : CharacterMovement
    {
        #region variables
        protected CharacterController _controller;
        public bool groundedPlayer { get; private set; } = false;
        private Vector3 playerVelocity = Vector3.zero;
        private float gravityValue;
        #endregion

        #region initialize
        /// <summary>
        /// Initiaze the movement of character.
        /// </summary>
        /// <param name="character"> the main class character. </param>
        public override void Initialize(Character character)
        {
            _controller = GetComponent<CharacterController>();
            gravityValue = Physics.gravity.y;
            base.Initialize(character);
        }
        #endregion

        #region move
        /// <summary>
        /// The function that controller the Move of character.
        /// </summary>
        /// <param name="deltaTime">Time delta between frames.</param>
        public override void Move(float deltaTime)
        {
            groundedPlayer = _controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (isInited && enableMove && AllowMove())
            {
                _controller.Move(direction * movingSpeed * deltaTime);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(playerVelocity * Time.deltaTime);
        }
        #endregion
    }
}
