using UnityEngine;

namespace characters
{
    [System.Serializable]
    public class CharacterData
    {
        public float hitPoint = 1;
        public float movingSpeed = 5;
        public float lookingSpeed = 5;

        public CharacterData ShallowCopy()
        {
            return (CharacterData)MemberwiseClone();
        }
    }

    [CreateAssetMenu(fileName = "New Character Settings", menuName = "Add/Characters/Character Settings", order = 200)]
    public class CharacterSettings : ScriptableObject
    {
        [SerializeField] protected CharacterTeam _myTeam;

        [SerializeField] protected CharacterData _defaultCharacterData;

        [Header("Settings")]
        [SerializeField] protected CharacterData _characterData;
        [SerializeField] protected string _characterType;

        #region gets
        public CharacterTeam myTeam => _myTeam;

        public string characterType => _characterType;

        public float hitPoint => _characterData.hitPoint;
        public float movingSpeed => _characterData.movingSpeed;
        public float lookingSpeed => _characterData.lookingSpeed;

        public CharacterData characterData => _characterData.ShallowCopy();
        #endregion

        public virtual void ResetData() => _characterData = _defaultCharacterData.ShallowCopy();

        protected virtual void OnValidate()
        {
            if (_characterType == null || _characterType.Length == 0)
                _characterType = name;
        }
    }
}