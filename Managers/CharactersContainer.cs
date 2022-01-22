using System.Collections.Generic;
using UnityEngine;

namespace characters
{
    [RequireComponent(typeof(Character))]
    public abstract class CharactersContainer : MonoBehaviour
    {
        [SerializeField] protected GameObject m_SubCharacterPrefab;

        protected List<ISubCharacter> subCharacters = new List<ISubCharacter>();

        public int totalSubs => subCharacters.Count;

        public ISubCharacter AddSubCharacter(Vector3 position)
        {
            if (m_SubCharacterPrefab == null) throw new System.NullReferenceException();

            ISubCharacter sub = Instantiate(m_SubCharacterPrefab,
                position,
                transform.rotation
                ).GetComponent<ISubCharacter>();

            sub.OnAdded(this, subCharacters.Count);
            subCharacters.Add(sub);
            return sub;
        }

        public void RemoveSubCharacter(int removeAmount = 1)
        {
            if (removeAmount <= 0) throw new System.ArgumentOutOfRangeException();

            int i = 0;
            subCharacters.RemoveAll((sub) =>
            {
                if (sub == null || sub.Equals(null))
                    return true;

                if (0 < removeAmount)
                {
                    sub.OnDeleted();
                    removeAmount--;
                    return true;
                }
                else
                {
                    sub.SetIdPlacement(i);
                    i++;
                    return false;
                }
            }
            );
        }

        public void RemoveSubCharacter(ISubCharacter subCharacter)
        {
            subCharacters.Remove(subCharacter);
        }

        protected virtual void OnValidate()
        {
            if (m_SubCharacterPrefab != null && m_SubCharacterPrefab.GetComponent<ISubCharacter>() == null)
            {
                Debug.LogError("Sub character prefab has not implementation of type ISubCharacter.");
                m_SubCharacterPrefab = null;
            }
        }
    }
}
