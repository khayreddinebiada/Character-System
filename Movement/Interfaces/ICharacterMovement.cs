using UnityEngine;

namespace characters
{
    public interface ICharacterMovement
    {
        bool enableMove { get; }

        bool enableLook { get; }

        void Initialize(Character character);

        void UpdateTransfrom(float deltaTime);
    }
}