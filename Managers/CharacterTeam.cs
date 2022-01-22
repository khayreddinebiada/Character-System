using UnityEngine;
using System.Collections.Generic;

namespace characters
{
    [CreateAssetMenu(fileName = "New Character Counter", menuName = "Add/Characters/Character Counter", order = 101)]
    public class CharacterTeam : ScriptableObject
    {
        [SerializeField] protected CharacterTeam _contenders;

        List<Character> characters = new List<Character>();

        public CharacterTeam contenders => _contenders;

        /// <summary>
        /// For add Characters in list.
        /// </summary>
        public void AddCharacter(Character character)
        {
            ClearNulls();

            characters.Add(character);
        }

        /// <summary>
        /// For Remove Character from list.
        /// </summary>
        public void RemoveCharacter(Character character)
        {
            characters.Remove(character);
        }

        public Character GetCharacterAt(int index)
        {
            return characters[index];
        }

        public Character GetNearest(Vector3 position)
        {
            bool hasNull = false;
            float distance = -1;
            Character character = null;

            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i] == null)
                {
                    hasNull = true;
                    continue;
                }

                float newDistance = (characters[i].transform.position - position).sqrMagnitude;
                if (distance == -1 || newDistance < distance)
                {
                    distance = newDistance;
                    character = characters[i];
                }
            }

            if (hasNull) ClearNulls();
            return character;
        }

        /// <summary>
        /// Clear all Characters from list.
        /// </summary>
        public void ClearNulls()
        {
            characters.RemoveAll(item => item == null || item.Equals(null));
        }
    }
}
