namespace characters
{
    public interface IFighterView : ICharacterView
    {
        void OnAttack(Character victim, float deltaTime = 1);

        void Kill(Character victim);
    }
}