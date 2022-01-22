using UnityEngine;

namespace characters
{
    public interface IDestination
    {
        Vector3 destination { get; }

        void SetDestination(Vector3 position);
        void OnReached();
    }
}
