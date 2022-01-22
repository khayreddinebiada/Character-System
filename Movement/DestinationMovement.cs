using System;
using UnityEngine;

namespace characters
{
    public abstract class MovementDestination : CharacterMovement, IDestination
    {
        #region delegates
        private event Action _onKill;
        public event Action onKill
        {
            add
            {
                _onKill += value;
            }
            remove
            {
                _onKill -= value;
            }
        }
#if UnityEvents
        [SerializeField] public UnityEngine.Events.UnityEvent onReached;
#endif
        #endregion

        #region variables
        public Vector3 destination { get; private set; }

        public virtual bool isReached
        {
            get
            {
                Vector3 last = (destination - m_LastPosition);
                last = new Vector3(last.x, 0, last.z);

                Vector3 current = (destination - transform.position);
                current = new Vector3(current.x, 0, current.z);

                return last.magnitude <= current.magnitude;
            }
        }
        private bool _isOnDestination = true;
        #endregion

        #region init
        /// <summary>
        /// Initiaze the movement of character.
        /// </summary>
        /// <param name="character"> the main class character. </param>
        public override void Initialize(Character character)
        {
            base.Initialize(character);
            destination = transform.position;
        }
        #endregion

        #region move
        /// <summary>
        /// This function is execute everytime when the AllowMove return true. You can handle the movement of character from here.
        /// </summary>
        public abstract void MoveHandle(float deltaTime);

        /// <summary>
        /// The function that controller the Move of character.
        /// </summary>
        /// <param name="deltaTime">Time delta between frames.</param>
        public override void Move(float deltaTime)
        {
            if (AllowMove() && isInited && enableMove)
            {
                MoveHandle(deltaTime);

                if (isReached && !_isOnDestination)
                {
                    _isOnDestination = true;
                    _onKill?.Invoke();
#if UnityEvents
                    onReached?.Invoke();
#endif
                    OnReached();
                }
            }
        }

        /// <summary>
        /// Set the destination where you want go. Next Position.
        /// </summary>
        public virtual void SetDestination(Vector3 destination)
        {
            this.destination = destination;
            _isOnDestination = false;
        }

        /// <summary>
        /// This function will executed when we reached the destination.
        /// </summary>
        public virtual void OnReached() { }
        #endregion
    }
}