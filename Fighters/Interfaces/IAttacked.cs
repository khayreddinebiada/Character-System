namespace characters
{
    public interface IAttacked
    {
        void Attacked(IDamage attacker, float deltaTime = 1);
        void Killed(IDamage killer);
    }
}