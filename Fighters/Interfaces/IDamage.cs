namespace characters
{
    public interface IDamage
    {
        Fighter fighter { get; }

        float value { get; }

        void OnAttack(Character victim);

        void OnKill(Character victim);
    }
}