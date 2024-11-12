using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class FactionSlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _enemyColor;
        [SerializeField] private Color _neutralColor;
        [SerializeField] private Color _allyColor;

        public void Setup(FactionRelationship fr)
        {
            _text.text = $"{fr.Faction.DisplayName}: <b><color=#{ColorUtility.ToHtmlStringRGBA(GetColor(fr.State))}>{fr.State}</color></b>";
        }

        private Color GetColor(RelationshipState state)
        {
            return state switch
            {
                RelationshipState.Enemy => _enemyColor,
                RelationshipState.Neutral => _neutralColor,
                RelationshipState.Ally => _allyColor,
                _ => Color.white,
            };
        }
    }
}