namespace characters
{
    public interface IFighter
    {
        bool enableAttack { get; set; }

        void Attack(Character victim = null, float deltaTime = 1);

        bool AllowAttack(Character victim);
    }
}