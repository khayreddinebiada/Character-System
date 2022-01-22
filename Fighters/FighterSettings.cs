using UnityEngine;

namespace characters
{
    [System.Serializable]
    public class FighterData
    {
        public float damage = 1;

        public FighterData ShallowCopy()
        {
            return (FighterData)MemberwiseClone();
        }
    }

    [CreateAssetMenu(fileName = "New Fighter Settings", menuName = "Add/Characters/Fighter Settings", order = 201)]
    public class FighterSettings : CharacterSettings
    {

        [Header("Fighter")]
        [SerializeField] protected FighterData _defaultFighterData;
        [SerializeField] protected FighterData _figherData;

        public CharacterTeam contenders => _myTeam.contenders;
        public FighterData fighterData => _figherData.ShallowCopy();

        public float damage => _figherData.damage;

        public override void ResetData()
        {
            base.ResetData();
            _figherData = _defaultFighterData.ShallowCopy();
        }
    }
}
