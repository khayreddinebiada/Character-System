using UnityEngine;

namespace characters
{
    public abstract class Fighter : Character, IFighter
    {
        #region delegates
        public delegate void AttackAction(Character victim, float damageValue, bool isKill);
        private event AttackAction _onAttackHandle;
        public event AttackAction onAttackHandle
        {
            add
            {
                _onAttackHandle += value;
            }
            remove
            {
                _onAttackHandle -= value;
            }
        }
        #endregion

        #region variables
        protected FighterSettings fighterSettings { get; private set; }
        protected Character contender { get; private set; }
        protected FighterData fighterData { get; private set; }

        public bool enableAttack { get; set; } = true;
        public bool hasContender => contender != null;
        protected IDamage m_Damage;
        #endregion

        #region inits
        internal override void DefineComponents()
        {
            base.DefineComponents();

            fighterSettings = _settings as FighterSettings;
            fighterData = fighterSettings.fighterData;

            m_Damage = DefineDamage();
            if (m_Damage == null) throw new System.NullReferenceException();
        }

        protected virtual IDamage DefineDamage() => new FighterDamage(this, fighterData.damage);
        #endregion

        #region Contender
        public void ChangeContender(Character contender)
        {
            if (contender == null || this.contender == contender)
                return;

            this.contender = contender;
        }
        #endregion

        #region attacks
        public void Attack(Character victim = null, float deltaTime = 1)
        {
            if (victim == null) victim = contender;

            if (enableAttack && victim != null && AllowAttack(victim))
            {
                victim.Attacked(m_Damage, deltaTime);
            }
        }

        internal void InvokeAttack(Character victim)
        {
            _onAttackHandle?.Invoke(victim, m_Damage.value, false);
            OnAttack(victim);
        }

        internal void InvokeKill(Character victim)
        {
            _onAttackHandle?.Invoke(victim, m_Damage.value, true);
            OnKill(victim);
        }

        protected virtual void OnKill(Character victim) { }

        protected virtual void OnAttack(Character victim) { }

        public virtual bool AllowAttack(Character victim) { return true; }
        #endregion

        #region editor
        protected virtual void OnValidate()
        {
            if (_settings != null && (_settings as FighterSettings) == null)
            {
                Debug.LogError("The settings inserted is not fighter settings please add figher settings");
                _settings = null;
            }
        }
        #endregion
    }
}