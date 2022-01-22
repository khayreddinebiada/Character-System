using UnityEngine;

namespace characters
{
    public class FighterDamage : IDamage
    {
        public FighterDamage(Fighter fighter, float value)
        {
            if (fighter == null) throw new System.ArgumentNullException();

            this.fighter = fighter;
            this.value = value;
        }

        public float value { get; set; }

        public Fighter fighter { get; protected set; }

        public void OnAttack(Character victim) => fighter.InvokeAttack(victim);

        public void OnKill(Character victim) => fighter.InvokeKill(victim);
    }
}