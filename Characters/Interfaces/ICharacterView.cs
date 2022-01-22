namespace characters
{
    public interface ICharacterView
    {
        void Initialize(Character character);
        void OnAttacked(IDamage attacker, float deltaTime = 1);
        void OnKilled(IDamage killer);
    }
}