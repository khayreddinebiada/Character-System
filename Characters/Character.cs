using System;
using UnityEngine;

namespace characters
{
    [RequireComponent(typeof(ICharacterMovement))]
    public abstract class Character : MonoBehaviour, IAttacked
    {
        #region delegates
        public delegate void AttackedAction(Fighter fighter, float hitPoint, bool isKilled);
        internal event AttackedAction _attackedHandle;
        public event AttackedAction attackedHandle
        {
            add
            {
                _attackedHandle += value;
            }
            remove
            {
                _attackedHandle -= value;
            }
        }
        #endregion

        #region variables
        [Header("Character settings")]
        [SerializeField] protected CharacterSettings _settings;

        public ICharacterMovement movement { get; private set; }
        internal CharacterData characterData { get; private set; }

        public CharacterSettings settings => _settings;
        public bool isDead { get; private set; } = false;
        public bool isInited { get; private set; } = false;
        #endregion

        #region initializes
        /// <summary>
        /// This function you call it when you want initialize the characters OnEnable() or Awake().
        /// </summary>
        protected void WakeUp()
        {
            DefineComponents();
            Initialize();
        }

        /// <summary>
        /// This function is execute the first one executed on init. For define the components.
        /// </summary>
        internal virtual void DefineComponents()
        {
            characterData = _settings.characterData ?? throw new NullReferenceException();
            movement = DefineCharacterMovement();
        }

        /// <summary>
        /// Here we initialize all character components and classes.
        /// </summary>
        internal virtual void Initialize()
        {
            isInited = true;

            _settings.myTeam.AddCharacter(this);

            movement?.Initialize(this);
        }

        protected virtual ICharacterMovement DefineCharacterMovement() => GetComponent<ICharacterMovement>();
        #endregion

        #region Attacked
        /// <summary>
        /// When we want make damage on the character call OnAttacked It will execute damage on character.
        /// </summary>
        /// <param name="damage">The character who will make damage</param>
        public void Attacked(IDamage damage, float deltaTime = 1)
        {
            if (isDead) return;
            if (isInited == false) throw new Exceptions.NonIntializedException();

            float lastHitPoint = characterData.hitPoint;
            characterData.hitPoint = Mathf.Clamp(
                (characterData.hitPoint - (damage.value * deltaTime))
                , 0, damage.value);


            if (characterData.hitPoint <= 0.0f) Killed(damage);
            else
            {
                damage.OnAttack(this);

                _attackedHandle?.Invoke(damage.fighter, characterData.hitPoint, false);

                OnAttacked(damage);
            }

        }

        protected virtual void OnAttacked(IDamage damage) { }

        /// <summary>
        /// This function executed when character dead or if you want.
        /// </summary>
        /// <param name="damage"> Character who kill this character. </param>
        public void Killed(IDamage damage)
        {
            if (isInited == false) throw new Exceptions.NonIntializedException();

            isDead = true;

            characterData.hitPoint = 0;

            _settings.myTeam.RemoveCharacter(this);

            damage.OnKill(this);

            _attackedHandle?.Invoke(damage.fighter, characterData.hitPoint, true);

            OnKilled(damage);
        }

        protected virtual void OnKilled(IDamage damage) { }
        #endregion
    }
}
