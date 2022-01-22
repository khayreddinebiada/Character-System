namespace characters
{
    public interface ISubCharacter
    {
        void OnAdded(CharactersContainer container, int idPlacement);

        void SetIdPlacement(int idPlacement);

        void OnDeleted();
    }
}
